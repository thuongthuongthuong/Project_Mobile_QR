using System.Runtime.InteropServices;

namespace Project_QR_BS.Data
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int cardState(byte step);

    public struct MFST_POS_INFO
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] sn; 		//!< Serial number				
        public byte initStatus;	//!< MPOS state.0x00: The work key has been filled 0x01:The master key is filled. 0x02: KEK has been modified. 0xFF: Default initial state
        public byte btype;			//!< MPOS power
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] posVer;	//!< MPOS terminal version number
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] dataVer;	//!< MPOS terminal data version number
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] customInfo;//!< Vendor custom information is used to store specific data
    };

    //Card info
    public struct MFST_CARD_INFO
    {
        public byte cardTimeout;        //!< Card timeout
        public byte cardmode;           //!< Card mode 0x01 swipe card. 0x02 IC. 0x04 RF. default 0x01|0x02|0x04
        public int amount;					//!< Transaction amount 
        public string transName;                //!< Transaction name
        public byte transtype;      //!< Transaction type
        public string tags;                     //!< IC card data TAG list
        public int tagslen;                 //!< TAG len
        public byte pinInput;           //!< enable the pin input
        public byte pinMaxLen;      //!< Enter the maximum length of pin
        public byte pinTimeout;		//!< Pin input time.
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] lsh;      //!< Trace No
        public string orderid;          //!< 
        public byte requiretype;        //!< 
        public byte allowfallback;    //!< Allow fallback
        public byte emvexectype;        //!< EMV execution mode
        public byte ecashpermit;        //!< 
        public byte forceonline;        //!< force online


        public cardState callBack; //!< //Card reading state//1 - Waiting for Card Swipe//2 - Reading Cards //3 - Waiting for User to Enter Password
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
        public byte[] recvdata; //!< 
        public int recvdatalen;             //!< 
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        public byte[] tlvval;               //!< 

    };


    // Return card info
    public struct MFST_RETURN_CARD_INFO
    {
        public byte cardType;           //!< 0x00 user cancelled operation, 0x01 Magnetic card, 0x02 IC card, 0x03 Tap card, 0x04 timeout, 0x05 read card error
        public byte track2Len;          //!< 2 track data length
        public byte track3Len;          //!< 3 track data length
        public byte fallback;           //!< Fallback
        public byte pinLen;         //!< Pin len
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] pan;			//!< Card number
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] expData;		//!< Card validity
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] serviceCode;	//!< Service code
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public byte[] track2;		//!< 2 track data
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public byte[] track3;		//!< 3 track data
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        public byte[] icData;		//!< Ic card data
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public byte[] pinblock;	//!< Cipher ciphertext
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] panSn;			//!< Card sn
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public byte[] ksn;			//!< ksn
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] mac;			//!< mac data
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] random;		//!< random
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] pinKsn;			//!< pinKsn
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] magKsn;			//!< magKsn
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] macKsn;           //!< macKsn
    };

    //Dukpt info
    public struct MFST_RETURN_DUKPT_INFO
    {
        public byte result;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8 + 1)]
        public byte[] checkValue;      //!< check value
    }
}
