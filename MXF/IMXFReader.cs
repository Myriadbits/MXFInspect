using Myriadbits.MXF.Identifiers;
using System;
using System.IO;

namespace Myriadbits.MXF
{
    public interface IMXFReader
    {

        public Stream Stream { get; }
        public bool EOF { get; }
        string FileName { get; set; }
        long Position { get; }
        long Size { get; } 
        void Close();
        void Open(string fileName);
        T[] ReadArray<T>(Func<T> readFunction, long count);
        AUID ReadAUID();
        MXFObject ReadAUIDSet(string groupName, string singleItem);
        MXFTimeStamp ReadBCDTimeCode(double frameRate);
        bool ReadBool();
        byte ReadByte();
        byte[] ReadBytes(int length);
        MXFChannelStatusMode ReadChannelstatusMode();
        MXFColorPrimary ReadColorPrimary();
        int ReadInt32();
        MXFProductVersion ReadProductVersion();
        MXFRational ReadRational();
        MXFReference<T> ReadReference<T>(string referringItemName) where T : MXFObject;
        MXFObject ReadReferenceSet<T>(string referringSetName, string singleItemName) where T : MXFObject;
        MXFRGBAComponent ReadRGBAComponent();
        MXFRGBAComponent[] ReadRGBALayout();
        sbyte ReadSignedByte();
        DateTime ReadTimestamp();
        ushort ReadUInt16();
        uint ReadUInt32();
        ulong ReadUInt64();
        UL ReadUL();
        UMID ReadUMIDKey();
        MXFUserDataMode ReadUserDataMode();
        string ReadUTF16String(int length);
        string ReadUTF8String(int length);
        UUID ReadUUID();
        MXFVersion ReadVersion();
        void Seek(long newPosition);
        //bool SeekForNextPotentialKey();
        void Skip(long toSkip);
    }
}