using System;
using System.Net;

namespace LangBox.Operaters
{
    internal class DownloadHelper
    {
        public static void DownLoadFileInBackground(string address, string filePath)
        {
            WebClient client = new WebClient();
            Uri uri = new Uri(address);

            // Specify a DownloadFileCompleted handler here...

            // Specify a progress notification handler.
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);

            client.DownloadFileAsync(uri, filePath);
        }

        private static void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            // Displays the operation identifier, and the transfer progress.
            Console.WriteLine("{0}    downloaded {1} of {2} bytes. {3} % complete...",
                (string)e.UserState,
                e.BytesReceived,
                e.TotalBytesToReceive,
                e.ProgressPercentage);
        }
    
}
}
