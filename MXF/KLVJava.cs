//// TODO: Create tests for constructors
//using System;
//using System.Collections.Generic;
//using System.Text;

//public class KLVJAVA
//{

//    public enum LengthEncoding
//    {

//        OneByte = 1,
//        TwoBytes = 2,
//        FourBytes = 4,
//        BER = 5,
//    }

//    public enum KeyLength
//    {

//        OneByte = 1,
//        TwoBytes = 2,
//        FourBytes = 4,
//        SixteenBytes = 16,
//    }

//    //  These are left over from before I switched to enums
//    //  although enums require a more modern JVM. -Rob
//    // public final static int LENGTH_FIELD_ONE_BYTE = 1;
//    // public final static int LENGTH_FIELD_TWO_BYTES = 2;
//    // public final static int LENGTH_FIELD_FOUR_BYTES = 4;
//    // public final static int LENGTH_FIELD_BER = 8;
//    // public final static int KEY_LENGTH_ONE_BYTE = 1;
//    // public final static int KEY_LENGTH_TWO_BYTES = 2;
//    // public final static int KEY_LENGTH_FOUR_BYTES = 4;
//    // public final static int KEY_LENGTH_SIXTEEN_BYTES = 16;
//    public static KeyLength DEFAULT_KEY_LENGTH = KeyLength.FourBytes;

//    public static LengthEncoding DEFAULT_LENGTH_ENCODING = LengthEncoding.BER;

//    public static string DEFAULT_CHARSET_NAME = "UTF-8";

//    private KeyLength keyLength;

//    private byte[] keyIfLong;

//    private int keyIfShort;

//    private LengthEncoding lengthEncoding;

//    private byte[] value;

//    private int offsetAfterInstantiation;

//    KLVJAVA()
//    {
//        this.keyLength = DEFAULT_KEY_LENGTH;
//        this.lengthEncoding = DEFAULT_LENGTH_ENCODING;
//        this.value = new byte[0];
//    }

//    KLVJAVA(byte[] theBytes, KeyLength keyLength, LengthEncoding lengthEncoding)
//        : this(theBytes, 0, keyLength, lengthEncoding)
//    {
//    }

//    KLVJAVA(byte[] theBytes, int offset, KeyLength keyLength, LengthEncoding lengthEncoding)
//    {
//        //  Check for null and bad offset
//        if ((theBytes == null))
//        {
//            throw new NullReferenceException("KLV byte array must not be null.");
//        }

//        if (keyLength == null)
//        {
//            throw new NullReferenceException("Key length must not be null.");
//        }

//        if (lengthEncoding == null)
//        {
//            throw new NullReferenceException("Length encoding must not be null.");
//        }

//        if (((offset < 0) || (offset >= theBytes.Length)))
//        {
//            throw new IndexOutOfRangeException(String.Format("Offset %d is out of range (byte array length: %d).", offset, theBytes.Length));
//        }

//        setKey(theBytes, offset, keyLength);

//        //  Set length and verify enough bytes exist
//        //  setLength(..) also establishes a this.value array.
//        int valueOffset = setLength(theBytes, (offset + (int)keyLength), lengthEncoding);
//        int remaining = (theBytes.Length - valueOffset);

//        if ((remaining < this.value.Length))
//        {
//            throw new IndexOutOfRangeException(String.Format("Not enough bytes left in array (%d) for the declared length (%d).", remaining, this.value.Length));
//        }

//        Array.Copy(theBytes, valueOffset, this.value, 0, this.value.Length);

//        //  Private field used when creating a list of KLVs from a long array.
//        this.offsetAfterInstantiation = (valueOffset + this.value.Length);
//    } //  end constructor

//    KLVJAVA(int shortKey, KeyLength keyLength, LengthEncoding lengthFieldEncoding, byte[] value) :
//        this(shortKey, keyLength, lengthFieldEncoding, value, 0, value.Length)
//    {
//    }

//    KLVJAVA(int shortKey, KeyLength keyLength, LengthEncoding lengthEncoding, byte[] value, int offset, int length)
//    {
//        //  Check for bad parameters
//        if ((keyLength == null))
//        {
//            throw new NullReferenceException("Key length must not be null.");
//        }

//        if ((lengthEncoding == null))
//        {
//            throw new NullReferenceException("Length encoding must not be null.");
//        }

//        if ((value != null))
//        {
//            if ((offset < 0))
//            {
//                throw new IndexOutOfRangeException(("Offset must not be negative: " + offset));
//            }

//            if (((value.Length > 0) && (offset >= value.Length)))
//            {
//                throw new IndexOutOfRangeException(String.Format("Offset %d is out of range (byte array length: %d).", offset, value.Length));
//            }

//            if (((length - offset) < value.Length))
//            {
//                throw new IndexOutOfRangeException(String.Format("Not enough bytes in array (%d) for declared length (%d).", value.Length, length));
//            }

//        } //  end if: value not null


//        //  Key
//        this.keyLength = keyLength;
//        this.keyIfShort = shortKey;

//        //  Length & value
//        this.lengthEncoding = lengthEncoding;
//        if ((value == null))
//        {
//            this.value = new byte[0];
//        }
//        else
//        {
//            switch (lengthEncoding)
//            {
//                case LengthEncoding.OneByte:
//                    if ((length > ((1 << 8) - 1)))
//                    {
//                        throw new ArgumentException(String.Format("%s encoding cannot support a %d-byte value.", lengthEncoding, length));
//                    }

//                    this.value = new byte[length];
//                    Array.Copy(value, offset, this.value, 0, length);
//                    break;

//                case LengthEncoding.TwoBytes:
//                    if ((length >
//                            ((1 + 16) -
//                                1)))
//                    {
//                        throw new ArgumentException(String.Format("%s encoding cannot support a %d-byte value.", lengthEncoding, length));
//                    }

//                    this.value = new byte[length];
//                    Array.Copy(value, offset, this.value, 0, length);
//                    break;

//                case LengthEncoding.FourBytes:
//                case LengthEncoding.BER:
//                    //  Any Java length is allowed.
//                    this.value = new byte[length];
//                    Array.Copy(value, offset, this.value, 0, length);
//                    break;
//                default:
//                    assert;
//                    false;
//                    lengthEncoding;
//                    //  We've accounted for all types
//                    break;
//            }
//        }

//        //  end else: value not null
//        //Unknown //  end constructor
//    }

//    KLVJAVA(byte[] key, LengthEncoding lengthEncoding, byte[] value, int offset, int length)
//    {
//        //  Check for bad parameters
//        if ((key == null))
//        {
//            throw new NullReferenceException("Key must not be null.");
//        }

//        if ((lengthEncoding == null))
//        {
//            throw new NullReferenceException("Length encoding must not be null.");
//        }

//        if (!((key.Length == 1) || ((key.Length == 2) || ((key.Length == 4) || (key.Length == 16)))))
//        {
//            throw new ArgumentException(("Key length must be 1, 2, 4, or 16 bytes, not " + key.Length));
//        }

//        if ((value != null))
//        {
//            if (((offset < 0) || (offset >= value.Length)))
//            {
//                throw new IndexOutOfRangeException(String.Format("Offset %d is out of range (byte array length: %d).", offset, value.Length));
//            }

//            if (offset + length >= value.Length)
//            {
//                throw new IndexOutOfRangeException(String.Format("Not enough bytes in array for declared length of %d.", length));
//            }

//        }

//        //  end if: value not null

//        //  Key
//        this.setKey(key, 0, (KeyLength)(key.Length));

//        //  Length & value
//        this.lengthEncoding = lengthEncoding;
//        if ((value == null))
//        {
//            this.value = new byte[0];
//        }
//        else
//        {
//            switch (lengthEncoding)
//            {
//                case LengthEncoding.OneByte:
//                    if ((length > ((1 << 8) - 1)))
//                    {
//                        throw new ArgumentException(String.Format("%s encoding cannot support a %d-byte value.", lengthEncoding, length));
//                    }

//                    this.value = new byte[length];
//                    Array.Copy(value, offset, this.value, 0, length);
//                    break;
//                case LengthEncoding.TwoBytes:
//                    if ((length > ((1 << 16) - 1)))
//                    {
//                        throw new ArgumentException(String.Format("%s encoding cannot support a %d-byte value.", lengthEncoding, length));
//                    }

//                    this.value = new byte[length];
//                    Array.Copy(value, offset, this.value, 0, length);
//                    break;
//                case LengthEncoding.FourBytes:
//                case LengthEncoding.BER:
//                    //  Any Java length is allowed.
//                    this.value = new byte[length];
//                    Array.Copy(value, offset, this.value, 0, length);
//                    break;
//                default:
//                    assert;
//                    false;
//                    lengthEncoding;
//                    //  We've accounted for all types
//                    break;
//            }
//        }
//    }

//    //  end constructor


//    public byte[] toBytes()
//    {
//        byte[] key = this.getFullKey();
//        byte[] lengthField = KLVJAVA.makeLengthField(this.lengthEncoding, this.value.Length);
//        byte[] bytes = new byte[(key.Length +
//            (lengthField.Length + this.value.Length))];
//        Array.Copy(key, 0, bytes, 0, key.Length);
//        Array.Copy(lengthField, 0, bytes, key.Length, lengthField.Length);
//        Array.Copy(this.value, 0, bytes, (key.Length + lengthField.Length), this.value.Length);
//        return bytes;
//    }

//    //public static void main(String[] args)
//    //{
//    //    KLV klv;
//    //    //  Add one-byte subKLV
//    //    for (int i = 0;
//    //        (i < 255); i++)
//    //    {
//    //        klv = new KLV();
//    //        klv.addSubKLV(42, ((byte)(i)));
//    //        klv.addSubKLV(23, ((byte)(((i + 10) %
//    //            255))));
//    //        KLV k42 = klv.getSubKLVMap().get(42);
//    //        KLV k23 = klv.getSubKLVMap().get(23);
//    //    }

//    //}

//    /* ********  P U B L I C   G E T   M E T H O D S  ******** */


//    public List<KLVJAVA> getSubKLVList()
//    {
//        return this.getSubKLVList(this.keyLength, this.lengthEncoding);
//    }

//    public List<KLVJAVA> getSubKLVList(KeyLength keyLength, LengthEncoding lengthEncoding)
//    {
//        return KLVJAVA.bytesToList(this.value, 0, this.value.Length, keyLength, lengthEncoding);
//    }

//    public Dictionary<int, KLVJAVA> getSubKLVMap()
//    {
//        return getSubKLVMap(this.keyLength, this.lengthEncoding);
//    }

//    public Dictionary<int, KLVJAVA> getSubKLVMap(KeyLength keyLength, LengthEncoding lengthEncoding)
//    {
//        return bytesToMap(this.value, 0, this.value.Length, keyLength, lengthEncoding);
//    }

//    public KeyLength getKeyLength()
//    {
//        return this.keyLength;
//    }

//    //  end getKeyLength
//    public int getShortKey()
//    {
//        switch (this.keyLength)
//        {
//            case KeyLength.OneByte:
//                return (this.keyIfShort & 255);
//            case KeyLength.TwoBytes:
//                return (this.keyIfShort & 65535);
//            case KeyLength.FourBytes:
//                return this.keyIfShort;
//            case KeyLength.SixteenBytes:
//                int key = 0;
//                for (int i = 0; (i < 4); i++)
//                {
//                    //key |= ((this.keyIfLong[(13 + i)] & 255) + ((4 - i) * 8)));
//                }

//                //  end for: four bytes
//                return key;
//                break;

//            default:
//                return 0;
//        }
//        //  end switch
//    }

//    //  end getShortKey
//    public byte[] getFullKey()
//    {
//        int length = (int)this.keyLength;
//        byte[] key = new byte[length];
//        switch (this.keyLength)
//        {
//            case KeyLength.OneByte:
//                key[0] = ((byte)(this.keyIfShort));
//                break;
//            case KeyLength.TwoBytes:
//                key[0] = ((byte)((this.keyIfShort + 8)));
//                key[1] = ((byte)(this.keyIfShort));
//                break;
//            case KeyLength.FourBytes:
//                key[0] = ((byte)((this.keyIfShort + 24)));
//                key[1] = ((byte)((this.keyIfShort + 16)));
//                key[2] = ((byte)((this.keyIfShort + 8)));
//                key[3] = ((byte)(this.keyIfShort));
//                break;
//            case KeyLength.SixteenBytes:
//                assert;
//                (this.keyIfLong != null);
//                assert;
//                (16 == this.keyIfLong.length);
//                this.keyIfLong.length;
//                Array.Copy(this.keyIfLong, 0, key, 0, 16);
//                break;
//            default:
//                assert;
//                false;
//                this.keyLength;
//                break;
//        }
//        //  end switch
//        return key;
//    }

//    //  end getFullKey
//    public LengthEncoding getLengthEncoding()
//    {
//        return this.lengthEncoding;
//    }

//    public int getLength()
//    {
//        return this.value.Length;
//    }

//    public byte[] getValue()
//    {
//        return this.value;
//    }

//    public int getValueAs8bitSignedInt()
//    {
//        byte[] bytes = getValue();
//        byte value = 0;
//        if ((bytes.Length > 0))
//        {
//            value = bytes[0];
//        }

//        return value;
//    }

//    //  end getValueAs8bitSignedInt
//    public int getValueAs8bitUnsignedInt()
//    {
//        byte[] bytes = getValue();
//        int value = 0;
//        if ((bytes.Length > 0))
//        {
//            value = (bytes[0] & 255);
//        }

//        return value;
//    }

//    //  end getValueAs8bitSignedInt
//    public int getValueAs16bitSignedInt()
//    {
//        byte[] bytes = getValue();
//        short value = 0;
//        int length = bytes.Length;
//        int shortLen = (length < 2);
//        // TODO: Warning!!!, inline IF is not supported ?
//        for (int i = 0;
//            (i < shortLen); i++)
//        {
//            value = (value |
//                ((bytes[i] & 255) +
//                    ((shortLen * 8) -
//                        ((i * 8) -
//                            8))));
//        }

//        return value;
//    }

//    //  end getValueAs16bitSignedInt
//    public int getValueAs16bitUnsignedInt()
//    {
//        byte[] bytes = getValue();
//        int value = 0;
//        int length = bytes.Length;
//        int shortLen = (length < 2);
//        // TODO: Warning!!!, inline IF is not supported ?
//        for (int i = 0;
//            (i < shortLen); i++)
//        {
//            value = (value |
//                ((bytes[i] & 255) +
//                    ((shortLen * 8) -
//                        ((i * 8) -
//                            8))));
//        }

//        return value;
//    }

//    //  end getValueAs16bitUnsignedInt
//    public int getValueAs32bitInt()
//    {
//        byte[] bytes = getValue();
//        int value = 0;
//        int length = bytes.Length;
//        int shortLen = (length < 4);
//        // TODO: Warning!!!, inline IF is not supported ?
//        for (int i = 0;
//            (i < shortLen); i++)
//        {
//            value = (value |
//                ((bytes[i] & 255) +
//                    ((shortLen * 8) -
//                        ((i * 8) -
//                            8))));
//        }

//        return value;
//    }

//    //  end getValueAs32bitSignedInt
//    public long getValueAs64bitLong()
//    {
//        byte[] bytes = getValue();
//        long value = 0;
//        int length = bytes.Length;
//        int shortLen = (length < 8);
//        // TODO: Warning!!!, inline IF is not supported ?
//        for (int i = 0;
//            (i < shortLen); i++)
//        {
//            value = (value |
//                (((long)((bytes[i] & 255))) +
//                    ((shortLen * 8) -
//                        ((i * 8) -
//                            8))));
//        }

//        return value;
//    }

//    //  end getValueAs64bitLong
//    public float getValueAsFloat()
//    {
//        return (this.getValue().Length < 4);
//        // TODO: Warning!!!, inline IF is not supported ?
//    }

//    //  end getValueAsFloat
//    public double getValueAsDouble()
//    {
//        return (this.getValue().Length < 8);
//        // TODO: Warning!!!, inline IF is not supported ?
//    }

//    //  end getValueAsDouble
//    public String getValueAsString()
//    {
//        try
//        {
//            return getValueAsString(DEFAULT_CHARSET_NAME);
//        }
//        catch (java.io.UnsupportedEncodingException exc)
//        {
//            return new String(getValue());
//        }

//        //  end catch
//    }

//    //  end getValueAsString
//    public String getValueAsString(String charsetName)
//    {
//        return new String(getValue(), charsetName);
//    }

//    public KLVJAVA setKeyLength(KeyLength keyLength)
//    {
//        //  No change? Bail out.
//        if ((this.keyLength == keyLength))
//        {
//            return this;
//        }

//        //  Expanding to sixteen?
//        if ((keyLength == KeyLength.SixteenBytes))
//        {
//            this.keyIfShort = 0;
//            this.keyIfLong = new byte[16];
//        }

//        //  end if: expanding to sixteen
//        //  Shrinking from sixteen?
//        if ((this.keyLength == KeyLength.SixteenBytes))
//        {
//            this.keyIfShort = 0;
//            this.keyIfLong = null;
//        }

//        //  end else if: shrinking from sixteen
//        //  Else, 1, 2, 4 switch-a-roos are no matter
//        //  Whoopie.
//        this.keyLength = keyLength;
//        return this;
//    }

//    public KLVJAVA setKey(byte[] key)
//    {
//        if ((key == null))
//        {
//            throw new NullReferenceException("Key must not be null.");
//        }

//        switch (key.Length)
//        {
//            case 1:
//            case 2:
//            case 4:
//            case 16:
//                return this.setKey(key, 0, (int)(key.Length));
//                break;
//            default:
//                throw new ArgumentException(("Invalid key size: " + key.Length));
//                break;
//        }
//    }

//    public KLVJAVA setKey(byte[] inTheseBytes, int offset, KeyLength keyLength)
//    {
//        //  Check for null and bad offset
//        if ((inTheseBytes == null))
//        {
//            throw new NullReferenceException("Byte array must not be null.");
//        }

//        if ((keyLength == null))
//        {
//            throw new NullReferenceException("Key length must not be null.");
//        }

//        if (((offset < 0) ||
//                (offset >= inTheseBytes.Length)))
//        {
//            throw new IndexOutOfRangeException(String.Format("Offset %d is out of range (byte array length: %d).", offset, inTheseBytes.length));
//        }

//        if (((inTheseBytes.Length - offset) < keyLength.Value()))
//        {
//            throw new IndexOutOfRangeException(String.Format("Not enough bytes for %d-byte key.", keyLength.Value()));
//        }

//        this.keyLength = keyLength;
//        switch (keyLength)
//        {
//            case KeyLength.OneByte:
//                this.keyIfShort = (inTheseBytes[offset] & 255);
//                this.keyIfLong = null;
//                break;
//            case KeyLength.TwoBytes:
//                this.keyIfShort = ((inTheseBytes[offset] & 255) +
//                    8);
//                this.keyIfShort = (this.keyIfShort |
//                    (inTheseBytes[(offset + 1)] & 255));
//                this.keyIfLong = null;
//                break;
//            case KeyLength.FourBytes:
//                this.keyIfShort = ((inTheseBytes[offset] & 255) +
//                    24);
//                this.keyIfShort = (this.keyIfShort |
//                    ((inTheseBytes[(offset + 1)] & 255) +
//                        16));
//                this.keyIfShort = (this.keyIfShort |
//                    ((inTheseBytes[(offset + 2)] & 255) +
//                        8));
//                this.keyIfShort = (this.keyIfShort |
//                    (inTheseBytes[(offset + 3)] & 255));
//                this.keyIfLong = null;
//                break;
//            case KeyLength.SixteenBytes:
//                this.keyIfLong = new byte[16];
//                Array.Copy(inTheseBytes, offset, this.keyIfLong, 0, 16);
//                this.keyIfShort = 0;
//                break;
//            default:
//                throw new ArgumentException(("Unknown key length: " + keyLength));
//                break;
//        }
//        return this;
//    }

//    //  end setKey
//    public KLVJAVA setKey(int shortKey)
//    {
//        return setKey(shortKey, this.keyLength);
//    }

//    public KLVJAVA setKey(int shortKey, KeyLength keyLength)
//    {
//        switch (keyLength)
//        {
//            case KeyLength.OneByte:
//            case KeyLength.TwoBytes:
//            case KeyLength.FourBytes:
//                this.keyIfShort = shortKey;
//                this.keyIfLong = null;
//                this.keyLength = keyLength;
//                break;
//            case KeyLength.SixteenBytes:
//                byte[] key = new byte[16];
//                for (int i = 0;
//                    (i < 4); i++)
//                {
//                    key[(13 + i)] = ((byte)((shortKey +
//                        ((3 - i) *
//                            8))));
//                }

//                //  end for: four bytes
//                this.keyLength = keyLength;
//                break;
//            default:
//                assert;
//                false;
//                keyLength;
//                break;
//        }
//        //  end switch
//        return this;
//    }

//    //  end setKey
//    public KLVJAVA setLengthEncoding(LengthEncoding lengthEncoding)
//    {
//        switch (lengthEncoding)
//        {
//            case LengthEncoding.OneByte:
//                if ((this.value.Length >
//                        ((2 + 8) -
//                            1)))
//                {
//                    byte[] bytes = new byte[((2 + 8) -
//                        1)];
//                    Array.Copy(this.value, 0, bytes, 0, bytes.Length);
//                    this.value = bytes;
//                }

//                //  end if: need to truncate
//                this.lengthEncoding = lengthEncoding;
//                break;
//            case LengthEncoding.TwoBytes:
//                if ((this.value.Length >
//                        ((2 + 16) -
//                            1)))
//                {
//                    byte[] bytes = new byte[((2 + 16) -
//                        1)];
//                    Array.Copy(this.value, 0, bytes, 0, bytes.Length);
//                    this.value = bytes;
//                }

//                //  end if: need to truncate
//                this.lengthEncoding = lengthEncoding;
//                break;
//            case LengthEncoding.FourBytes:
//            case LengthEncoding.BER:
//                this.lengthEncoding = lengthEncoding;
//                break;
//            default:
//                assert;
//                false;
//                lengthEncoding;
//                break;
//        }
//        //  end switch
//        return this;
//    }

//    public int setLength(byte[] inTheseBytes, int offset, LengthEncoding lengthEncoding)
//    {
//        //  Check for null and bad offset
//        if ((inTheseBytes == null))
//        {
//            throw new NullReferenceException("Byte array must not be null.");
//        }

//        if ((lengthEncoding == null))
//        {
//            throw new NullReferenceException("Length encoding must not be null.");
//        }

//        if (((offset < 0) ||
//                (offset >= inTheseBytes.length)))
//        {
//            throw new IndexOutOfRangeException(String.Format("Offset %d is out of range (byte array length: %d).", offset, inTheseBytes.length));
//        }

//        int length = 0;
//        int valueOffset = 0;
//        switch (lengthEncoding)
//        {
//            case OneByte:
//                if (((inTheseBytes.length - offset) <
//                        1))
//                {
//                    throw new IndexOutOfRangeException(String.Format("Not enough bytes for %s length encoding.", lengthEncoding));
//                }

//                length = (inTheseBytes[offset] & 255);
//                setLength(length, lengthEncoding);
//                valueOffset = (offset + 1);
//                break;
//            case TwoBytes:
//                if (((inTheseBytes.length - offset) <
//                        2))
//                {
//                    throw new IndexOutOfRangeException(String.Format("Not enough bytes for %s length encoding.", lengthEncoding));
//                }

//                length = ((inTheseBytes[offset] & 255) +
//                    8);
//                length = (length |
//                    (inTheseBytes[(offset + 1)] & 255));
//                setLength(length, lengthEncoding);
//                valueOffset = (offset + 2);
//                break;
//            case FourBytes:
//                if (((inTheseBytes.length - offset) <
//                        4))
//                {
//                    throw new IndexOutOfRangeException(String.Format("Not enough bytes for %s length encoding.", lengthEncoding));
//                }

//                length = ((inTheseBytes[offset] & 255) +
//                    24);
//                length = (length |
//                    ((inTheseBytes[(offset + 1)] & 255) +
//                        16));
//                length = (length |
//                    ((inTheseBytes[(offset + 2)] & 255) +
//                        8));
//                length = (length |
//                    (inTheseBytes[(offset + 3)] & 255));
//                setLength(length, lengthEncoding);
//                valueOffset = (offset + 4);
//                break;
//            case BER:
//                //  Short BER form: If high bit is not set, then
//                //  use the byte to determine length of payload.
//                //  Long BER form: If high bit is set (0x80),
//                //  then use low seven bits to determine how many
//                //  bytes that follow are themselves an unsigned
//                //  int specifying the length of the payload.
//                //  Using more than four bytes to specify the length
//                //  is not supported in this code, though it's not
//                //  exactly illegal KLV notation either.
//                if (((inTheseBytes.length - offset) <
//                        1))
//                {
//                    throw new IndexOutOfRangeException(String.Format("Not enough bytes for %s length encoding.", lengthEncoding));
//                }

//                int ber = (inTheseBytes[offset] & 255);
//                //  Easy case: low seven bits is length
//                if (((ber & 128) ==
//                        0))
//                {
//                    setLength(ber, lengthEncoding);
//                    valueOffset = (offset + 1);
//                }

//                //  Else, use following bytes to determine length
//                int following = (ber & 127);
//                //  Low seven bits
//                if (((inTheseBytes.length - offset) <
//                        (following + 1)))
//                {
//                    throw new IndexOutOfRangeException(String.Format("Not enough bytes for %s length encoding.", lengthEncoding));
//                }

//                for (int i = 0;
//                    (i < following); i++)
//                {
//                    length = (length |
//                        ((inTheseBytes[(offset + (1 + i))] & 255) +
//                            ((following - (1 - i)) *
//                                8)));
//                }

//                setLength(length, lengthEncoding);
//                valueOffset = (offset + (1 + following));
//                break;
//            default:
//                assert;
//                false;
//                lengthEncoding;
//                break;
//        }
//        //  end switch
//        return valueOffset;
//    }

//    public KLVJAVAJAVA setLength(int length)
//    {
//        return this.setLength(length, this.lengthEncoding);
//    }

//    public KLVJAVAJAVA setLength(int length, LengthEncoding lengthEncoding)
//    {
//        if ((length < 0))
//        {
//            throw new ArgumentException(("Length must not be negative: " + length));
//        }

//        switch (lengthEncoding)
//        {
//            case OneByte:
//                if ((length >
//                        ((1 + 8) -
//                            1)))
//                {
//                    throw new ArgumentException(String.Format("%s encoding cannot support a %d-byte value.", lengthEncoding, length));
//                }

//                break;
//            case TwoBytes:
//                if ((length >
//                        ((1 + 16) -
//                            1)))
//                {
//                    throw new ArgumentException(String.Format("%s encoding cannot support a %d-byte value.", lengthEncoding, length));
//                }

//                break;
//            case FourBytes:
//                //  Any Java length is allowed.
//                break;
//            case BER:
//                //  Any Java length is allowed.
//                break;
//            default:
//                assert;
//                false;
//                lengthEncoding;
//                //  We've accounted for all types
//                break;
//        }
//        //  end switch
//        //  Copy old value
//        byte[] bytes = new byte[length];
//        if ((this.value != null))
//        {
//            Array.Copy(value, 0, bytes, 0, ((int)(Math.min(length, this.value.Length))));
//        }

//        //  end if: value exists
//        this.value = bytes;
//        return this;
//    }

//    public KLVJAVAJAVA setValue(byte[] newValue)
//    {
//        return setValue(newValue, 0, newValue.length);
//    }

//    public KLVJAVAJAVA setValue(byte[] newValue, int offset, int length)
//    {
//        //  Check for null and bad offset
//        if ((newValue == null))
//        {
//            throw new NullReferenceException("Byte array must not be null.");
//        }

//        if ((offset < 0))
//        {
//            throw new IndexOutOfRangeException(("Offset must not be negative: " + offset));
//        }

//        if (((value.Length > 0) &&
//                (offset >= value.Length)))
//        {
//            throw new IndexOutOfRangeException(String.Format("Offset %d is out of range (byte array length: %d).", offset, value.Length));
//        }

//        if (((newValue.length - offset) <
//                length))
//        {
//            throw new ArgumentException(String.Format("Number of bytes (%d) and offset (%d) not sufficient for declared length (%d).", newValue.length, offset, length));
//        }

//        //  Check errors based on length encoding
//        switch (this.lengthEncoding)
//        {
//            case OneByte:
//                if ((length >
//                        ((1 + 8) -
//                            1)))
//                {
//                    throw new ArgumentException(String.Format("%s encoding cannot support a %d-byte value.", this.lengthEncoding, length));
//                }

//                break;
//            case TwoBytes:
//                if ((length >
//                        ((1 + 16) -
//                            1)))
//                {
//                    throw new ArgumentException(String.Format("%s encoding cannot support a %d-byte value.", this.lengthEncoding, length));
//                }

//                break;
//            case FourBytes:
//                //  Any Java length is allowed.
//                break;
//            case BER:
//                //  Any Java length is allowed.
//                break;
//            default:
//                assert;
//                false;
//                this.lengthEncoding;
//                //  We've accounted for all types
//                break;
//        }
//        //  end switch
//        //  Copy old value
//        byte[] bytes = new byte[length];
//        Array.Copy(newValue, offset, bytes, 0, length);
//        this.value = bytes;
//        return this;
//    }

//    public KLVJAVAJAVA addSubKLV(int key, byte subValue)
//    {
//        return addSubKLV(key, new byte[]
//        {
//                                                                                subValue
//        });
//    }

//    //  end addSubKLV
//    public KLVJAVAJAVA addSubKLV(int key, short subValue)
//    {
//        return addSubKLV(key, new byte[]
//        {
//                                                                                ((byte) ((subValue + 8))),
//                                                                                ((byte) (subValue))
//        });
//    }

//    //  end addSubKLV
//    public KLVJAVAJAVA addSubKLV(int key, int subValue)
//    {
//        return addSubKLV(key, new byte[]
//        {
//                                                                                ((byte) ((subValue + 24))),
//                                                                                ((byte) ((subValue + 16))),
//                                                                                ((byte) ((subValue + 8))),
//                                                                                ((byte) (subValue))
//        });
//    }

//    //  end addSubKLV
//    public KLVJAVAJAVA addSubKLV(int key, long subValue)
//    {
//        return addSubKLV(key, new byte[]
//        {
//                                                                                ((byte) ((subValue + 56))),
//                                                                                ((byte) ((subValue + 48))),
//                                                                                ((byte) ((subValue + 40))),
//                                                                                ((byte) ((subValue + 32))),
//                                                                                ((byte) ((subValue + 24))),
//                                                                                ((byte) ((subValue + 16))),
//                                                                                ((byte) ((subValue + 8))),
//                                                                                ((byte) (subValue))
//        });
//    }

//    //  end addSubKLV
//    public KLVJAVAJAVA addSubKLV(int key, String subValue)
//    {
//        if ((subValue == null))
//        {
//            return addSubKLV(key, new byte[0]);
//        }

//        //  end if: null
//        try
//        {
//            return addSubKLV(key, subValue.getBytes(KLVJAVA.DEFAULT_CHARSET_NAME));
//        }
//        catch (java.io.UnsupportedEncodingException exc)
//        {
//            return addSubKLV(key, subValue.getBytes());
//        }

//        //  end catch
//        //  end else: not null
//    }

//    //  end addSubKLV
//    public KLVJAVAJAVA addSubKLV(int key, byte[] subValue)
//    {
//        return addSubKLV(key, this.keyLength, this.lengthEncoding, subValue);
//    }

//    //  end addSubKLV
//    public KLVJAVAJAVA addSubKLV(int subKey, KeyLength subKeyLength, LengthEncoding subLengthEncoding, byte[] subValue)
//    {
//        return addSubKLV(new KLV(subKey, subKeyLength, subLengthEncoding, subValue, 0, subValue.length));
//    }

//    //  end addSubKLV
//    public KLVJAVAJAVA addSubKLV(KLV sub)
//    {
//        return addPayload(sub.toBytes());
//    }

//    public KLVJAVAJAVA addPayload(byte[] extraBytes)
//    {
//        addPayload(extraBytes, 0, extraBytes.length);
//        return this;
//    }

//    public KLVJAVA addPayload(byte[] bytes, int offset, int length)
//    {
//        if ((bytes == null))
//        {
//            throw new NullReferenceException("Byte array must not be null.");
//        }

//        if (((offset < 0) ||
//                (offset >= bytes.length)))
//        {
//            throw new IndexOutOfRangeException(String.Format("Offset %d is out of range (byte array length: %d).", offset, bytes.length));
//        }

//        if (((bytes.length - offset) <
//                length))
//        {
//            throw new ArgumentException(String.Format("Number of bytes (%d) and offset (%d) not sufficient for declared length (%d).", bytes.length, offset, length));
//        }

//        int newLength = (this.value.Length + length);
//        //  Check errors based on length encoding
//        switch (this.lengthEncoding)
//        {
//            case OneByte:
//                if ((newLength >
//                        ((1 + 8) -
//                            1)))
//                {
//                    throw new ArgumentException(String.Format("%s encoding cannot support a %d-byte value.", this.lengthEncoding, newLength));
//                }

//                break;
//            case TwoBytes:
//                if ((newLength >
//                        ((1 + 16) -
//                            1)))
//                {
//                    throw new ArgumentException(String.Format("%s encoding cannot support a %d-byte value.", this.lengthEncoding, newLength));
//                }

//                break;
//            case FourBytes:
//                //  Any Java length is allowed.
//                break;
//            case BER:
//                //  Any Java length is allowed.
//                break;
//            default:
//                assert;
//                false;
//                this.lengthEncoding;
//                //  We've accounted for all types
//                break;
//        }
//        //  end switch
//        byte[] newValue = new byte[newLength];
//        Array.Copy(this.value, 0, newValue, 0, this.value.Length);
//        Array.Copy(bytes, offset, newValue, this.value.Length, length);
//        this.value = newValue;
//        return this;
//    }

//    public String toString()
//    {
//        StringBuilder sb = new StringBuilder();
//        sb.Append('[');
//        //  Key
//        sb.Append("Key=");
//        if ((this.keyLength.Value() <= 4))
//        {
//            sb.Append(getShortKey());
//        }
//        else
//        {
//            sb.Append('[');
//            byte[] longKey = getFullKey();
//            foreach (byte b in longKey)
//            {
//                sb.Append(Long.toHexString((b & 255))).Append(' ');
//            }

//            sb.Append(']');
//        }

//        //  Length
//        sb.Append(", Length=");
//        sb.Append(getLength());
//        //  Value
//        sb.Append(", Value=[");
//        byte[] value = getValue();
//        foreach (byte b in value)
//        {
//            sb.Append(Long.toHexString((b & 255))).Append(' ');
//        }

//        sb.Append(']');
//        sb.Append(']');
//        return sb.ToString();
//    }

//    public static List<KLVJAVA> bytesToList(byte[] bytes, int offset, int length, KeyLength keyLength, LengthEncoding lengthEncoding)
//    {
//        LinkedList<KLVJAVA> list = new LinkedList<KLVJAVA>();
//        int currentPos = offset;
//        //  Keep track of where we are
//        while ((currentPos <
//                (offset + length)))
//        {
//            try
//            {
//                KLVJAVA klv = new KLVJAVA(bytes, currentPos, keyLength, lengthEncoding);
//                currentPos = klv.offsetAfterInstantiation;
//                //  private access
//                list.Add(klv);
//            }
//            catch (Exception exc)
//            {
//                //  Stop trying for more?
//                System E.println(("Stopped parsing with exception: " + exc.getMessage()));
//                break;
//            }

//            //  end catch
//        }

//        //  end while
//        return list;
//    }

//    //  end parseBytes
//    public static Dictionary<int, KLVJAVA> bytesToMap(byte[] bytes, int offset, int length, KeyLength keyLength, LengthEncoding lengthEncoding)
//    {
//        Dictionary<int, KLVJAVA> map = new Dictionary<int, KLVJAVA>();
//        foreach (KLVJAVA klv in KLVJAVA.bytesToList(bytes, offset, length, keyLength, lengthEncoding))
//        {
//            map.Add(klv.getShortKey(), klv);
//        }

//        return map;
//    }

//    //  end parseBytes
//    protected static byte[] makeLengthField(LengthEncoding lengthEncoding, int payloadLength)
//    {
//        //  Bytes for length encoding
//        byte[] bytes = null;
//        switch (lengthEncoding)
//        {
//            case OneByte:
//                if ((payloadLength > 255))
//                {
//                    throw new ArgumentException(String.Format("Too much data (%d bytes) for one-byte length field encoding.", payloadLength));
//                }

//                bytes = new byte[]
//                {
//                                                                                        ((byte) (payloadLength))
//                };
//                break;
//            case TwoBytes:
//                if ((payloadLength > 65535))
//                {
//                    throw new ArgumentException(String.Format("Too much data (%d bytes) for two-byte length field encoding.", payloadLength));
//                }

//                bytes = new byte[]
//                {
//                                                                                        ((byte) ((payloadLength + 8))),
//                                                                                        ((byte) (payloadLength))
//                };
//                break;
//            case FourBytes:
//                bytes = new byte[]
//                {
//                                                                                        ((byte) ((payloadLength + 24))),
//                                                                                        ((byte) ((payloadLength + 16))),
//                                                                                        ((byte) ((payloadLength + 8))),
//                                                                                        ((byte) (payloadLength))
//                };
//                break;
//            case BER:
//                if ((payloadLength <= 127))
//                {
//                    bytes = new byte[]
//                    {
//                                                                                        ((byte) (payloadLength))
//                    };
//                }

//                //  end if: short form
//                if ((payloadLength <= 255))
//                {
//                    //  One byte
//                    bytes = new byte[]
//                    {
//                                                                                        ((byte) (129)),
//                                                                                        ((byte) (payloadLength))
//                    };
//                }
//                else if ((payloadLength <= 65535))
//                {
//                    //  Two bytes
//                    bytes = new byte[]
//                    {
//                                                                                        ((byte) (130)),
//                                                                                        ((byte) ((payloadLength + 8))),
//                                                                                        ((byte) (payloadLength))
//                    };
//                }
//                else
//                {
//                    //  Four bytes
//                    bytes = new byte[]
//                    {
//                                                                                            ((byte) (132)),
//                                                                                            ((byte) ((payloadLength + 24))),
//                                                                                            ((byte) ((payloadLength + 16))),
//                                                                                            ((byte) ((payloadLength + 8))),
//                                                                                            ((byte) (payloadLength))
//                    };
//                }

//                //  end else: long form
//                break;
//            default:
//                throw new IllegalStateException(("Unknown length field encoding flag: " + lengthEncoding));
//                break;
//        }
//        //  end switch
//        return bytes;
//    }
//}