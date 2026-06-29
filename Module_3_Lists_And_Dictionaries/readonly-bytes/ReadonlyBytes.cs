using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace hashes
{
    public class ReadonlyBytes : IEnumerable<byte>
    {
        private byte[] bytes { get; }
        public int Length { get { return bytes.Length; } }

        private readonly int Hash;

        public ReadonlyBytes(params byte[] bytes)
        {
            if (bytes == null) throw new ArgumentNullException(nameof(bytes));

            int offset_basis = unchecked((int)146959846656037);
            this.bytes = bytes;
            foreach (byte b in bytes)
                unchecked
                {
                    Hash = Hash * offset_basis + b;
                }
        }

        public byte this[int index]
        {
            get
            {
                if (index < 0 || index >= Length) throw new IndexOutOfRangeException();
                return bytes[index];
            }
        }

        public override int GetHashCode()
        {
            return Hash;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ReadonlyBytes) || (obj.GetType() != this.GetType()) || (ReferenceEquals(null, obj)))
                return false;

            return Hash == ((ReadonlyBytes)obj).Hash;
        }

        public override string ToString()
        {
            string strByte = null;
            int index = 0;
            foreach (byte b in bytes)
            {
                if (index == 0)
                    strByte += b.ToString();
                else
                {
                    strByte += ", " + b.ToString();
                }
                index++;
            }
            return "[" + strByte + "]";
        }

        public IEnumerator<byte> GetEnumerator()
        {
            for (int i = 0; i < Length; i++)
                yield return bytes[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}