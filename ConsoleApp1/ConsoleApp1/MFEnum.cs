using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ConsoleApp1
{
    public enum MFEU_MAINKEY_ENCRYPT
    {
        KEK = 0x00,         //!< Using KEK encryption
        MAINKEY = 0x01,     //!< The original encryption key
        PLAINTEXT = 0x02,   //!< Use plain master key
        KEK_SN = 0x03,      //!< Uses dual encryption of KEK and SN
        KEK_FIXED = 0x04,   //!< Use KEK FIXED

    };

    public enum MFEU_AID_ACTION
    {
        AID_CLEAR = 0x01,       //!< Clear all key
        AID_ADD = 0x02,         //!< Add a key
        AID_DELETE = 0x03,      //!< Delete a key
        AID_READLIST = 0x04,    //!< Get key list
        AID_READAPPOINT = 0x05  //!< Get a key

    };

    public enum MFEU_PUK_ACTION
    {
        PUK_CLEAR = 0x01,       //!< Clear all key
        PUK_ADD = 0x02,         //!< Add a key
        PUK_DELETE = 0x03,      //!< Delete a key
        PUK_READLIST = 0x04,    //!< Get key list
        PUK_READAPPOINT = 0x05  //!< Get a key
    };

    //DUKPT
    public enum MFEU_DUKPT_TYPE
    {

        DUKPT_IPEK_PLAIN = 0x00,    //!< IPEK plaintext
        DUKPT_BDK_PLAIN = 0x01,     //!< BDK plaintext
        DUKPT_IPEK_ENC_KEK = 0x02,  //!< IPEK ciphertext encrypt by KEK
        DUKPT_BDK_ENC_KEK = 0x03,   //!< BDK ciphertext encrypt by KEK
        DUKPT_IPEK_ENC_MAK = 0x04,  //!< IPEK ciphertext encrypt by master key encrypt
        DUKPT_BDK_ENC_MAK = 0x05,   //!< BDK ciphertext encrypt by master key encrypt

    }

    //KEY_INDEX
    public enum MFEU_KEY_INDEX
    {

        KEY_INDEX_0 = 0x00,
        KEY_INDEX_1 = 0x01,
        KEY_INDEX_2 = 0x02,
        KEY_INDEX_3 = 0x03,
        KEY_INDEX_4 = 0x04,
        KEY_INDEX_5 = 0x05,
        KEY_INDEX_6 = 0x06,
        KEY_INDEX_7 = 0x07,

    }
}
