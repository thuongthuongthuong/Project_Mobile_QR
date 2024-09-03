using Project_QR_BS.Data;
using Project_QR_BS.Models;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Project_QR_BS.Services
{
    public class POSService
    {
        private const string LogDirectory = "Log_QR";

        [DllImport("mpos_sdk.dll", EntryPoint = "mf_init", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mf_init(int id, int mode);

        [DllImport("mpos_sdk.dll", EntryPoint = "mf_connect", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mf_connect();

        [DllImport("mpos_sdk.dll", EntryPoint = "mf_readPosInfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mf_readPosInfo(IntPtr posInfo);

        [DllImport("mpos_sdk.dll", EntryPoint = "mf_setDatetime", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mf_setDatetime(string dateTime);

        [DllImport("mpos_sdk.dll", EntryPoint = "mf_showText", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mf_showText(byte timeOut, string text, int len);

        [DllImport("mpos_sdk.dll", EntryPoint = "mf_genQrCode", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mf_genQrCode(byte timeOut, byte[] qrCode, int len);

        [DllImport("mpos_sdk.dll", EntryPoint = "mf_resetPos", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mf_resetPos();

        // Example placeholder for mf_playAudio, assuming it exists
        [DllImport("mpos_sdk.dll", EntryPoint = "mf_playAudio", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mf_playAudio(string status);

        public ApiResponseStatus ConnectToBS()
        {
            init();
            int connectionResult = mf_connect();
            LogConnectionResult(connectionResult);
            return connectionResult == 1 ? ApiResponseStatus.Success : ApiResponseStatus.Failure;
        }

        public ApiResponseStatus DisplayQRCode(DisplayQRCodeRequest request)
        {
            // Concatenate QRInfo and Money using the format QRInfo|money
            string combinedInfo = $"{request.QRInfo}|{request.Money}";
            byte[] encodedBytes = Encoding.UTF8.GetBytes(combinedInfo);
            int displayResult = mf_genQrCode(30, encodedBytes, encodedBytes.Length);
            LogQRCodeGeneration(request.Id, request.Money,request.QRInfo);
            return displayResult == 1 ? ApiResponseStatus.Success : ApiResponseStatus.Failure;
        }

        public ApiResponseStatus DisplayPaymentStatus(string status)
        {
            int statusResult = mf_showText(30, status, status.Length);
            LogPaymentStatus(status);
            
            // Wait for 2 seconds before resetting the POS
            System.Threading.Thread.Sleep(3000);

            // Reset the POS
            mf_resetPos();

            return statusResult == 1 ? ApiResponseStatus.Success : ApiResponseStatus.Failure;
        }

        private void init()
        {
            try
            {
                int initResult = mf_init(0, 0);
                LogInitialization(initResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during POS initialization: {ex.Message}");
                throw new Exception($"An error occurred during POS initialization: {ex.Message}", ex);
            }
        }

        public ApiResponseStatus ResetPOS()
        {
            try
            {
                int resetResult = mf_resetPos();
                LogResetAction(resetResult);
                return resetResult == 1 ? ApiResponseStatus.Success : ApiResponseStatus.Failure;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during POS reset: {ex.Message}");
                return ApiResponseStatus.Failure;
            }
        }

        public ApiResponseStatus CheckConnection()
        {
            // Use the current datetime as a parameter for mf_setDatetime
            string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");

            // Call the mf_setDatetime function to check the connection
            int result = mf_setDatetime(currentDateTime);

            // Return success if the result is 0, failure otherwise
            return result == 0 ? ApiResponseStatus.Success : ApiResponseStatus.Failure;
        }

        private void LogResetAction(int resetResult)
        {
            string logFilePath = GetLogFilePath();
            AppendLog(logFilePath, $"{GetCurrentTime()} ------------reset------------");
            AppendLog(logFilePath, $"{GetCurrentTime()} resetResult={resetResult}");
        }

        private void LogInitialization(int initResult)
        {
            string logFilePath = GetLogFilePath();
            AppendLog(logFilePath, $"{GetCurrentTime()} ------------init------------");
            AppendLog(logFilePath, $"{GetCurrentTime()} initResult={initResult}");
        }

        private void LogConnectionResult(int connectionResult)
        {
            string logFilePath = GetLogFilePath();
            AppendLog(logFilePath, $"{GetCurrentTime()} ------------connect------------");
            AppendLog(logFilePath, $"{GetCurrentTime()} connectionResult={connectionResult}");
        }

        private void LogQRCodeGeneration(string id,string money,string qrInfo)
        {
            string logFilePath = GetLogFilePath();
            AppendLog(logFilePath, $"{GetCurrentTime()} ------------genQR------------");
            AppendLog(logFilePath, $"{GetCurrentTime()} id={id}");
            AppendLog(logFilePath, $"{GetCurrentTime()} stringQR={qrInfo}");
            AppendLog(logFilePath, $"{GetCurrentTime()} Money={money}");
        }

        private void LogPaymentStatus(string status)
        {
            string logFilePath = GetLogFilePath();
            AppendLog(logFilePath, $"{GetCurrentTime()} -------Status------------");
            AppendLog(logFilePath, $"{GetCurrentTime()} status={status}");
        }

        private string GetLogFilePath()
        {
            // Ensure Log directory exists
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
            }

            // Generate the log file path with the current timestamp
            string logFileName = $"log_{DateTime.Now.ToString("yyyyMMdd")}.txt";
            return Path.Combine(LogDirectory, logFileName);
        }

        private void AppendLog(string filePath, string logEntry)
        {
            // Append log entry to the file
            File.AppendAllText(filePath, logEntry + Environment.NewLine);
        }

        private string GetCurrentTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
