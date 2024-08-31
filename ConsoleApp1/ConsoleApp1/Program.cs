using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        private static bool isOpen = false;

        [DllImport("mpos_sdk.dll", EntryPoint = "mf_init", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mf_init(int id, int mode);

        [DllImport("mpos_sdk.dll", EntryPoint = "mf_set_manufacturer_id", CallingConvention = CallingConvention.Cdecl)]
        public static extern void mf_set_manufacturer_id(int id);

        [DllImport("mpos_sdk.dll", EntryPoint = "mf_connect", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mf_connect();

        [DllImport("mpos_sdk.dll", EntryPoint = "mf_readPosInfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mf_readPosInfo(IntPtr posInfo);

        [DllImport("mpos_sdk.dll", EntryPoint = "mf_card_exec", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mf_card_exec(IntPtr param, IntPtr returnInfo);

        [DllImport("mpos_sdk.dll", EntryPoint = "mf_setKeyIndex", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mf_setKeyIndex(byte mainkeyindex);

        [DllImport("mpos_sdk.dll", EntryPoint = "mf_loadMasterkey", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mf_loadMasterkey(MFEU_MAINKEY_ENCRYPT type, byte mainkeyindex, byte[] key);

        [DllImport("mpos_sdk.dll", EntryPoint = "mf_loadworkkey", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mf_loadworkkey(byte mainkeyindex, byte[] pinKey, byte[] macKey, byte[] trackKey);

        [DllImport("mpos_sdk.dll", EntryPoint = "mf_aidManager", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mf_aidManager(MFEU_AID_ACTION action, string aid);

        [DllImport("mpos_sdk.dll", EntryPoint = "mf_pukManager", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mf_pukManager(MFEU_PUK_ACTION action, string puk);

        [DllImport("mpos_sdk.dll", EntryPoint = "mf_LoadDukpt", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mf_LoadDukpt(MFEU_DUKPT_TYPE type, byte mainKeyIndex, string key, string ksn, IntPtr returnInfo);

        [DllImport("mpos_sdk.dll", EntryPoint = "mf_setDatetime", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mf_setDatetime(string dateTime);

        [DllImport("mpos_sdk.dll", EntryPoint = "mf_showText", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mf_showText(byte timeOut, string text, int len);

        [DllImport("mpos_sdk.dll", EntryPoint = "mf_showQrCode", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mf_showQrCode(byte timeOut, string qrCode);

        [DllImport("mpos_sdk.dll", EntryPoint = "mf_genQrCode", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mf_genQrCode(byte timeOut, byte[] qrCode, int len);

        [DllImport("mpos_sdk.dll", EntryPoint = "mf_resetPos", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mf_resetPos();

        [DllImport("mpos_sdk.dll", EntryPoint = "mf_playAudio", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mf_playAudio(byte playType, byte playTimes, int playDelay);
        public static int callBack(byte step)
        {
            switch (step)
            {
                case 1:
                    Console.WriteLine("Waiting for Card Swipe");
                    break;
                case 2:
                    Console.WriteLine("Reading Cards");
                    break;
                case 3:
                    Console.WriteLine("Waiting for User to Enter Password");
                    break;
                default:
                    Console.WriteLine("");
                    break;

            }
            return 0;

        }
        static void Main(string[] args)
        {
            init();

            Console.WriteLine("======================MFMpos API Sample=====================");
            Console.WriteLine("Please select the function to be tested:");
            Console.WriteLine("1:Connect");
            Console.WriteLine("2:Read PosInfo");
            Console.WriteLine("3:Read Card");
            Console.WriteLine("4:Load MaterKey");
            Console.WriteLine("5:Load WorkKey");
            Console.WriteLine("6:Download AID");
            Console.WriteLine("7:Download PUK");
            Console.WriteLine("8:Load Dukpt");
            Console.WriteLine("9:Set Time");
            Console.WriteLine("10:Show Text");
            Console.WriteLine("11:Show QrCode");
            Console.WriteLine("12:Set KeyIndex");
            Console.WriteLine("13:Gen QrCode");
            Console.WriteLine("14:Reset");
            Console.WriteLine("15:Play Audio");
            while (true)
            {
                String key = Console.ReadLine();

                if (key.Equals("1"))
                {
                    connect();
                }
                else
                {
                    if (!isOpen)
                    {
                        Console.WriteLine("Please connect device first");
                    }
                    else
                    {
                        switch (key)
                        {
                            case "2":
                                readPosInfo();
                                break;
                            case "3":
                                readCard();
                                break;
                            case "4":
                                loadMasterKey();
                                break;
                            case "5":
                                loadWorkKey();
                                break;
                            case "6":
                                aidManager();
                                break;
                            case "7":
                                pukManager();
                                break;
                            case "8":
                                loadDukpt();
                                break;
                            case "9":
                                setDatetime();
                                break;
                            case "10":
                                showText();
                                break;
                            case "11":
                                showQrCode();
                                break;
                            case "12":
                                setKeyIndex();
                                break;
                            case "13":
                                Console.WriteLine("Input string unicode: ");
                                string unicode = "";
                                unicode = Console.ReadLine();
                                genQrCode(unicode);
                                break;
                            case "14":
                                reset();
                                break;
                            case "15":
                                playAudio();
                                break;

                        }
                    }

                }

            }
        }


        private static void init()
        {
            mf_init(0, 0);
        }
        private static void connect()
        {
            int ret = mf_connect();
            if (ret == 1)
            {
                isOpen = true;
            }
            Console.WriteLine("mf_connect ret:" + ret);

        }

        private static void setKeyIndex()
        {
            Console.WriteLine("============setKeyIndex================");
            mf_setKeyIndex(0x0);
        }

        private static void loadDukpt()
        {
            Console.WriteLine("============loadDukpt================");

            byte mainKeyIndex = 0;
            string key = "C1D0F8FB4958670DBA40AB1F3752EF0D";
            string ksn = "110000F15CAD88000006";

            MFST_RETURN_DUKPT_INFO dukptInfo = new MFST_RETURN_DUKPT_INFO();

            int size = Marshal.SizeOf(dukptInfo);
            IntPtr intPtr = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(dukptInfo, intPtr, true);
                int ret = mf_LoadDukpt(MFEU_DUKPT_TYPE.DUKPT_BDK_PLAIN, 0x0, key, ksn, intPtr);
                MFST_RETURN_DUKPT_INFO returnInfo = (MFST_RETURN_DUKPT_INFO)Marshal.PtrToStructure(intPtr, typeof(MFST_RETURN_DUKPT_INFO));

                Console.WriteLine("CheckValue:" + HexUtils.BytesToHexString(returnInfo.checkValue));
                Console.WriteLine("Result:" + returnInfo.result);
                Console.WriteLine("======================================");
            }
            catch (ArgumentException)
            {
                throw;
            }
            finally
            {
                Marshal.FreeHGlobal(intPtr);    //free tha memory
            }

        }

        private static void readPosInfo()
        {

            MFST_POS_INFO posInfo = new MFST_POS_INFO();

            int size = Marshal.SizeOf(posInfo);
            IntPtr intPtr = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(posInfo, intPtr, true);
                mf_readPosInfo(intPtr);
                MFST_POS_INFO ret = (MFST_POS_INFO)Marshal.PtrToStructure(intPtr, typeof(MFST_POS_INFO));

                Console.WriteLine("============readPosInfo================");
                Console.WriteLine("DataVer:" + System.Text.Encoding.Default.GetString(ret.dataVer));
                Console.WriteLine("PosVer:" + System.Text.Encoding.Default.GetString(ret.posVer));
                Console.WriteLine("SN:" + System.Text.Encoding.Default.GetString(ret.sn));
                Console.WriteLine("Battery:" + Convert.ToString(ret.btype));
                Console.WriteLine("Status:" + Convert.ToString(ret.initStatus));
                Console.WriteLine("======================================");
            }
            catch (ArgumentException)
            {
                throw;
            }
            finally
            {
                Marshal.FreeHGlobal(intPtr);    //free tha memory
            }

        }

        private static void loadMasterKey()
        {
            Console.WriteLine("============loadMasterKey================");
            byte[] key = { 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x40, 0x82, 0x6A, 0x58 };

            int ret = mf_loadMasterkey(MFEU_MAINKEY_ENCRYPT.PLAINTEXT, 0x0, key);
            Console.WriteLine("loadMasterKey ret:" + ret);
        }

        private static void loadWorkKey()
        {
            Console.WriteLine("============loadWorkKey================");
            byte[] pinKey = { 0x40, 0x82, 0x6A, 0x58, 0x00, 0x60, 0x8C, 0x87, 0x40, 0x82, 0x6A, 0x58, 0x00, 0x60, 0x8C, 0x87, 0x8C, 0xA6, 0x4D, 0xE9 };
            byte[] macKey = { 0x40, 0x82, 0x6A, 0x58, 0x00, 0x60, 0x8C, 0x87, 0x40, 0x82, 0x6A, 0x58, 0x00, 0x60, 0x8C, 0x87, 0x8C, 0xA6, 0x4D, 0xE9 };
            byte[] trackKey = { 0x40, 0x82, 0x6A, 0x58, 0x00, 0x60, 0x8C, 0x87, 0x40, 0x82, 0x6A, 0x58, 0x00, 0x60, 0x8C, 0x87, 0x8C, 0xA6, 0x4D, 0xE9 };

            int ret = mf_loadworkkey(0x0, pinKey, macKey, trackKey);
            Console.WriteLine("mf_loadworkkey ret:" + ret);
        }

        private static void aidManager()
        {
            Console.WriteLine("============Download AID================");
            string[] aids = new string[] {
                //Union Pay
                "9F0608A000000333010100DF0101009F08020020DF1105D84000A800DF1205D84004F800DF130500100000009F1B0400000000DF150400000000DF160199DF170199DF14039F3704DF1801019F7B06000000200000DF1906000000200000DF2006000002000000DF2106000000100000",
                "9F0608A000000333010101DF0101009F08020020DF1105D84000A800DF1205D84004F800DF130500100000009F1B0400000000DF150400000000DF160199DF170199DF14039F3704DF1801019F7B06000000200000DF1906000000200000DF2006000002000000DF2106000000100000",
                "9F0608A000000333010102DF0101009F08020020DF1105D84000A800DF1205D84004F800DF130500100000009F1B0400000000DF150400000000DF160199DF170199DF14039F3704DF1801019F7B06000000200000DF1906000000200000DF2006000002000000DF2106000000100000",
                "9F0608A000000333010103DF0101009F08020020DF1105D84000A800DF1205D84004F800DF130500100000009F1B0400000000DF150400000000DF160199DF170199DF14039F3704DF1801019F7B06000000200000DF1906000000200000DF2006000002000000DF2106000000100000",
                //Visa
                "9F0607A0000000031010DF0101009F08020020DF1105D84000A800DF1205D84004F800DF130500100000009F1B0400000000DF150400000000DF160199DF170199DF14039F3704DF1801019F7B06000000200000DF1906000000200000DF2006000002000000DF2106000000100000",
                //Master
                "9F0607A0000000041010DF0101009F08020020DF1105D84000A800DF1205D84004F800DF130500100000009F1B0400000000DF150400000000DF160199DF170199DF14039F3704DF1801019F7B06000000200000DF1906000000200000DF2006000002000000DF2106000000100000",
                //Local Master
                "9F0607D4100000012010DF0101009F08020020DF1105D84000A800DF1205D84004F800DF130500100000009F1B0400000000DF150400000000DF160199DF170199DF14039F3704DF1801019F7B06000000200000DF1906000000200000DF2006000002000000DF2106000000100000",
                //Local Visa
                "9F0607D4100000011010DF0101009F08020020DF1105D84000A800DF1205D84004F800DF130500100000009F1B0400000000DF150400000000DF160199DF170199DF14039F3704DF1801019F7B06000000200000DF1906000000200000DF2006000002000000DF2106000000100000",
                //AMERICAN EXPRESS
                "9F0608A000000025010402DF0101009F08020020DF1105D84000A800DF1205D84004F800DF130500100000009F1B0400000000DF150400000000DF160199DF170199DF14039F3704DF1801019F7B06000000200000DF1906000000200000DF2006000002000000DF2106000000100000",
                //AMERICAN EXPRESS
                "9F0608A000000025010501DF0101009F08020020DF1105D84000A800DF1205D84004F800DF130500100000009F1B0400000000DF150400000000DF160199DF170199DF14039F3704DF1801019F7B06000000200000DF1906000000200000DF2006000002000000DF2106000000100000",
                //JCB
                "9F0607A0000000651010DF0101009F08020020DF1105D84000A800DF1205D84004F800DF130500100000009F1B0400000000DF150400000000DF160199DF170199DF14039F3704DF1801019F7B06000000200000DF1906000000200000DF2006000002000000DF2106000000100000",
                //D-PAS
                "9F0607A0000001523010DF0101009F08020020DF1105D84000A800DF1205D84004F800DF130500100000009F1B0400000000DF150400000000DF160199DF170199DF14039F3704DF1801019F7B06000000200000DF1906000000200000DF2006000002000000DF2106000000100000",
                //Rupay
                "9F0607A0000005241010DF0101009F08020002DF11050000000000DF12050000000000DF130500000000009F1B04000186A0DF14039F3704DF150400000000DF160105DF170100DF1801319F7B06000000200000DF1906000000200000DF2006000002000000DF2106000000100000",
                "9F0608A000000524010101DF0101009F08020030DF11050000000000DF12050000000000DF130500000000009F1B04000186A0DF150400000000DF160100DF170100DF14039F3704DF1801319F7B06000000010000DF1906000000010000DF2006000000050000DF2106000000004000",
                "9F0607A0000005241011DF0101009F08020002DF11050000000000DF12050000000000DF130500000000009F1B04000186A0DF14039F3704DF150400000000DF160105DF170100DF1801319F7B06000000200000DF1906000000200000DF2006000002000000DF2106000000100000",
            };

            for (int i = 0; i < aids.Length; i++)
            {
                Console.WriteLine("Download AID " + i);
                mf_aidManager(MFEU_AID_ACTION.AID_ADD, aids[i]);
            }
        }

        private static void pukManager()
        {
            Console.WriteLine("============Download PUK================");
            string[] rids = new string[]{
                    "9F0605A0000003339F220102DF050420211231DF060101DF070101DF028190A3767ABD1B6AA69D7F3FBF28C092DE9ED1E658BA5F0909AF7A1CCD907373B7210FDEB16287BA8E78E1529F443976FD27F991EC67D95E5F4E96B127CAB2396A94D6E45CDA44CA4C4867570D6B07542F8D4BF9FF97975DB9891515E66F525D2B3CBEB6D662BFB6C3F338E93B02142BFC44173A3764C56AADD202075B26DC2F9F7D7AE74BD7D00FD05EE430032663D27A57DF040103DF031403BB335A8549A03B87AB089D006F60852E4B8060",
                    "9F0605A0000003339F220103DF050420221231DF060101DF070101DF0281B0B0627DEE87864F9C18C13B9A1F025448BF13C58380C91F4CEBA9F9BCB214FF8414E9B59D6ABA10F941C7331768F47B2127907D857FA39AAF8CE02045DD01619D689EE731C551159BE7EB2D51A372FF56B556E5CB2FDE36E23073A44CA215D6C26CA68847B388E39520E0026E62294B557D6470440CA0AEFC9438C923AEC9B2098D6D3A1AF5E8B1DE36F4B53040109D89B77CAFAF70C26C601ABDF59EEC0FDC8A99089140CD2E817E335175B03B7AA33DDF040103DF031487F0CD7C0E86F38F89A66F8C47071A8B88586F26",
                    "9F0605A0000003339F220104DF050420221231DF060101DF070101DF0281F8BC853E6B5365E89E7EE9317C94B02D0ABB0DBD91C05A224A2554AA29ED9FCB9D86EB9CCBB322A57811F86188AAC7351C72BD9EF196C5A01ACEF7A4EB0D2AD63D9E6AC2E7836547CB1595C68BCBAFD0F6728760F3A7CA7B97301B7E0220184EFC4F653008D93CE098C0D93B45201096D1ADFF4CF1F9FC02AF759DA27CD6DFD6D789B099F16F378B6100334E63F3D35F3251A5EC78693731F5233519CDB380F5AB8C0F02728E91D469ABD0EAE0D93B1CC66CE127B29C7D77441A49D09FCA5D6D9762FC74C31BB506C8BAE3C79AD6C2578775B95956B5370D1D0519E37906B384736233251E8F09AD79DFBE2C6ABFADAC8E4D8624318C27DAF1DF040103DF0314F527081CF371DD7E1FD4FA414A665036E0F5E6E5"
            };

            for (int i = 0; i < rids.Length; i++)
            {
                Console.WriteLine("Download PUK " + i);
                mf_pukManager(MFEU_PUK_ACTION.PUK_ADD, rids[i]);
            }
        }

        private static void readCard()
        {
            Console.WriteLine("============readCard================");

            string defaultTags = "9F269F279F109F379F36959A9C9F025F2A829F1A9F039F339F349F35849F419F099F1E9F63";

            MFST_CARD_INFO param = new MFST_CARD_INFO();
            MFST_RETURN_CARD_INFO returnInfo = new MFST_RETURN_CARD_INFO();

            param.transName = "Sale";
            param.cardmode = 7;
            param.cardTimeout = 60;
            param.transtype = 0x00;
            param.tags = defaultTags;
            param.tagslen = defaultTags.Length;
            param.emvexectype = 0x06;
            param.ecashpermit = 0x00;
            param.forceonline = 0x31;
            param.pinInput = 0x01;
            param.pinMaxLen = 0x06;
            param.pinTimeout = 60;
            param.allowfallback = 0x01;
            param.requiretype = 0x00;
            param.orderid = "1234567890";
            param.lsh = System.Text.Encoding.Default.GetBytes("123456");
            param.amount = 2;
            param.callBack = callBack;

            int paramSize = Marshal.SizeOf(param);
            IntPtr intPtr1 = Marshal.AllocHGlobal(paramSize);

            int returnSize = Marshal.SizeOf(returnInfo);
            IntPtr intPtr2 = Marshal.AllocHGlobal(returnSize);

            try
            {
                Marshal.StructureToPtr(param, intPtr1, false);
                Marshal.StructureToPtr(returnInfo, intPtr2, false);

                mf_card_exec(intPtr1, intPtr2);

                MFST_RETURN_CARD_INFO returnCardInfo = (MFST_RETURN_CARD_INFO)Marshal.PtrToStructure(intPtr2, typeof(MFST_RETURN_CARD_INFO));

                Console.WriteLine("cardType:" + returnCardInfo.cardType);
                Console.WriteLine("pan:" + System.Text.Encoding.Default.GetString(returnCardInfo.pan));
                Console.WriteLine("expData:" + System.Text.Encoding.Default.GetString(returnCardInfo.expData));
                Console.WriteLine("serviceCode:" + System.Text.Encoding.Default.GetString(returnCardInfo.serviceCode));
                Console.WriteLine("track2Len:" + returnCardInfo.track2Len);
                Console.WriteLine("track3Len:" + returnCardInfo.track3Len);
                Console.WriteLine("track2:" + System.Text.Encoding.Default.GetString(returnCardInfo.track2));
                Console.WriteLine("track3:" + System.Text.Encoding.Default.GetString(returnCardInfo.track3));
                Console.WriteLine("randomdata:" + System.Text.Encoding.Default.GetString(returnCardInfo.random));
                Console.WriteLine("icData:" + System.Text.Encoding.Default.GetString(returnCardInfo.icData));
                Console.WriteLine("fallback:" + returnCardInfo.fallback);
                Console.WriteLine("pinLen:" + returnCardInfo.pinLen);
                Console.WriteLine("pinblock:" + System.Text.Encoding.Default.GetString(returnCardInfo.pinblock));
                Console.WriteLine("pansn:" + System.Text.Encoding.Default.GetString(returnCardInfo.panSn));
                Console.WriteLine("ksn:" + System.Text.Encoding.Default.GetString(returnCardInfo.ksn));
                Console.WriteLine("mac:" + System.Text.Encoding.Default.GetString(returnCardInfo.mac));
                Console.WriteLine("pinKsn:" + System.Text.Encoding.Default.GetString(returnCardInfo.pinKsn));
                Console.WriteLine("macKsn:" + System.Text.Encoding.Default.GetString(returnCardInfo.macKsn));
                Console.WriteLine("magKsn:" + System.Text.Encoding.Default.GetString(returnCardInfo.magKsn));

            }
            catch (ArgumentException)
            {
                throw;
            }
            finally
            {
                Marshal.FreeHGlobal(intPtr1);    //free tha memory
                Marshal.FreeHGlobal(intPtr2);    //free tha memory
            }

        }

        private static void setDatetime()
        {
            Console.WriteLine("============SetDatetime================");
            string dateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            int ret = mf_setDatetime(dateTime);
        }

        private static void showText()
        {
            Console.WriteLine("============showText================");
            string text = "Successful";
            int ret = mf_showText(30, text, text.Length);
            Console.WriteLine("showText ret:" + ret);
        }

        private static void showQrCode()
        {
            Console.WriteLine("============Show QrCode================");
            string qrCode = "https://example.com"; // Thay bằng QR code hợp lệ
            int ret = mf_showQrCode(10, qrCode);
            Console.WriteLine("mf_showQrCode ret:" + ret);

            if (ret != 0)
            {
                Console.WriteLine("Failed to show QR code. Error code: " + ret);
            }
        }


        private static void genQrCode(string unicodeString)
        {
            Console.WriteLine("============genQrCode================");

            // Define the Unicode string that needs to be encoded into a QR code
            //string unicodeString = "00020101021238560010A0000007270126000697041501121280001129930208QRIBFTTA530370454065000005802VN62310105313640818 BN 31364 tam ung.630435C0|500.000|Lê Chiêu Tân";

            // Convert the string into a byte array encoded in UTF-8
            byte[] encodedBytes = Encoding.UTF8.GetBytes(unicodeString);

            // Call the mf_genQrCode function from the external library
            int ret = mf_genQrCode(30, encodedBytes, encodedBytes.Length);

            // Output the result of the QR code generation process
            Console.WriteLine("genQrCode ret:" + ret);
        }

        private static void reset()
        {
            Console.WriteLine("============reset================");
            int ret = mf_resetPos();
            Console.WriteLine("reset ret:" + ret);
        }

        private static void playAudio()
        {
            Console.WriteLine("============playAudio================");

            // Example: play a beep sound 3 times with a 500 ms delay between each.
            byte playType = 0; // Start with 0 and test other values
            byte playTimes = 1; // Set to 1 for a single playback
            int playDelay = 100; // Minimal delay

            int ret = mf_playAudio(playType, playTimes, playDelay);
            Console.WriteLine("playAudio ret:" + ret);

            if (ret != 0)
            {
                Console.WriteLine("Failed to play audio. Error code: " + ret);
            }
        }

    }
}

