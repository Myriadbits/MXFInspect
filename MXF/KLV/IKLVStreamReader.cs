using Myriadbits.MXF.Identifiers;
using System;
using System.IO;

namespace Myriadbits.MXF.KLV
{
    public interface IKLVStreamReader
    {
        #region properties

        bool EOF { get; }
        long Position { get; }

        #endregion

        #region methods

        void Seek(long newPosition);

        #endregion

        #region primitives

        bool ReadBoolean();
        byte ReadByte();
        sbyte ReadSByte();
        byte[] ReadBytes(int length);
        ushort ReadUInt16();
        uint ReadUInt32();
        int ReadInt32();
        ulong ReadUInt64();
        string ReadUTF16String(int length);
        string ReadUTF8String(int length);
        #endregion

        #region ref types

        UL ReadUL();
        AUID ReadAUID();
        UMID ReadUMIDKey();
        UUID ReadUUID();
        MXFRational ReadRational();
        DateTime ReadTimestamp();
        MXFProductVersion ReadProductVersion();
        MXFUserDataMode ReadUserDataMode();
        MXFChannelStatusMode ReadChannelstatusMode();
        MXFColorPrimary ReadColorPrimary();
        T[] ReadArray<T>(Func<T> readFunction, long count);
        MXFObject ReadAUIDSet(string groupName, string singleItem);
        MXFTimeStamp ReadBCDTimeCode(double frameRate);
        MXFVersion ReadVersion();
        MXFReference<T> ReadReference<T>(string referringItemName) where T : MXFObject;
        MXFObject ReadReferenceSet<T>(string referringSetName, string singleItemName) where T : MXFObject;
        MXFRGBAComponent ReadRGBAComponent();
        MXFRGBAComponent[] ReadRGBALayout();

        #endregion
    }
}