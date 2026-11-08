using System;
using System.IO;
using System.Net;
using System.Text;

namespace AhoyBrowser
{
    class HttpRequest
    {
        private const string Identifier = "ahoyv"; // Must be available in the remote file
        private const string UrlAddress = "https://ahoynote.org/utility/check-90240a01-db96-4e34-a51e-9cf99456d8e3.html"; // http path
        internal const string DownloadPageUrl = "https://ahoynote.org/utility/download.html";
        private static int _newVersionNumber; // Holds the received version number
        private static int _currentVersionNumber; // Holds the current version number
        internal static int CheckForUpdates(int updateCheckCode)
        {
            /*
             *
                0 = No connection/failed
                1 = Your version is up-to-date
                2 = New update available. Visit download page?
             *
             */

            Log.UpdateStaus("Contacting server...");
            string data = "No Data";
            try
            {

                if (updateCheckCode == 0)
                {
                    // Get current version number as int
                    _currentVersionNumber = Convert.ToInt32(Splash.AhoyTruncatedVersionNumber); // 1500; (test)
                }
                else if (updateCheckCode == 1)
                {
                    _currentVersionNumber = 0;
                }
                else if (updateCheckCode == 2)
                {
                    _currentVersionNumber = 9999;
                }
                else
                {
                    return 0; // Failed
                }

                // using System.Net;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                // Use SecurityProtocolType.Ssl3 if needed for compatibility reasons

                // Do request
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UrlAddress);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Log.UpdateStaus("Status OK");
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    if (response.CharacterSet == null)
                    {
                        readStream = new StreamReader(receiveStream);
                    }
                    else
                    {
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }

                    data = readStream.ReadToEnd();

                    if (data.Length == 0)
                    {
                        data = "Response empty";
                    }
                    else
                    {
                        data = data.ToLower().Trim();
                    }

                    response.Close();
                    readStream.Close();
                }
                else
                {
                    Log.UpdateStaus("Connection failed");
                }
            }
            catch (Exception e)
            {
                data = e.Message;
            }

            // If data was received check for the identifier and remove it to reveal the version number
            if (data.Contains(Identifier))
            {
                try
                {
                    Log.UpdateStaus("Response validated");
                    // Extract version number and compare to current number
                    data = data.Replace(Identifier, "");
                    _newVersionNumber = Convert.ToInt32(data);
                    if (_newVersionNumber > _currentVersionNumber)
                    {
                        Log.UpdateStaus("v" + data + " (New version)");
                        return 2; // New update available. Visit download page?
                    }
                    else
                    {
                        Log.UpdateStaus("v" + data + " (Version current)");
                        return 1; // Your version is up - to - date
                    }
                }
                catch
                {
                    Log.UpdateStaus("Unable to resolve data");
                    Log.UpdateStaus(data);
                    return 0;// No connection/failed
                }
            }
            Log.UpdateStaus(data);
            return 0;
        }
    }
}
