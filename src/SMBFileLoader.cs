using SMBLibrary.Client;
using SMBLibrary;
using System;
using System.Collections.Generic;
using System.Net;

namespace SMBMediaHub
{
    internal class SMBFileLoader
    {
        public static List<string> LoadFilesFromSMB(string serverIp, string username, string password, string shareName)
        {
            List<string> fileNames = []; // Initialize the list of file names

            SMB2Client client = new();
            bool isConnected = client.Connect(IPAddress.Parse(serverIp), SMBTransportType.DirectTCPTransport);
            if (isConnected)
            {
                NTStatus status = client.Login(String.Empty, username, password);
                if (status == NTStatus.STATUS_SUCCESS)
                {
                    ISMBFileStore fileStore = client.TreeConnect(shareName, out status);
                    if (status == NTStatus.STATUS_SUCCESS)
                    {
                        status = fileStore.CreateFile(
                            out object directoryHandle,
                            out FileStatus fileStatus,
                            String.Empty,
                            AccessMask.GENERIC_READ,
                            FileAttributes.Directory,
                            ShareAccess.Read | ShareAccess.Write,
                            CreateDisposition.FILE_OPEN,
                            CreateOptions.FILE_DIRECTORY_FILE,
                            null);

                        if (status == NTStatus.STATUS_SUCCESS)
                        {
                            // Query the directory and list files
                            status = fileStore.QueryDirectory(out List<QueryDirectoryFileInformation> fileList, directoryHandle, "*", FileInformationClass.FileDirectoryInformation);

                            if (status == NTStatus.STATUS_SUCCESS)
                            {
                                // Loop through the file list and extract the file names
                                foreach (var fileInfo in fileList)
                                {
                                    if (fileInfo is FileDirectoryInformation dirInfo) // Cast to the appropriate subclass
                                    {
                                        // Now we can access the 'FileName' property
                                        fileNames.Add(dirInfo.FileName);
                                    }
                                }
                            }
                            fileStore.CloseFile(directoryHandle);
                        }
                        fileStore.Disconnect();
                    }
                    client.Logoff();
                }
                client.Disconnect();
            }

            return fileNames;
        }
    }
}