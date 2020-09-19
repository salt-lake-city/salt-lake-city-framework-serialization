using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaltLakeCity.Framework.Serialization
{
    public class ByteSerializer
    {
        /// <summary>
        ///     Buffer, in den beim Serialisieren geschrieben wird
        /// </summary>
        private readonly List<byte> _buffer;

        /// <summary>
        ///     Buffer der genutzt wird, wenn Deserialisiert wird
        /// </summary>
        private readonly byte[] _readableBuffer;

        /// <summary>
        ///     Position auf dem ReadableBuffer
        /// </summary>
        private int _readPos;

        public ByteSerializer(List<byte> buffer)
        {
            _buffer = buffer;
            _readableBuffer = buffer.ToArray();
        }

        public ByteSerializer() : this(new List<byte>())
        {
        }

        public ByteSerializer(byte[] buffer) : this(buffer.ToList())
        {
        }


        #region Write Data

        /// <summary>Adds a byte to the packet.</summary>
        /// <param name="value">The byte to add.</param>
        public void Write(byte value)
        {
            _buffer.Add(value);
        }

        /// <summary>Adds an array of bytes to the packet.</summary>
        /// <param name="value">The byte array to add.</param>
        public void Write(byte[] value)
        {
            _buffer.AddRange(value);
        }

        /// <summary>Adds a short to the packet.</summary>
        /// <param name="value">The short to add.</param>
        public void Write(short value)
        {
            _buffer.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>Adds an int to the packet.</summary>
        /// <param name="value">The int to add.</param>
        public void Write(int value)
        {
            _buffer.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>Adds a long to the packet.</summary>
        /// <param name="value">The long to add.</param>
        public void Write(long value)
        {
            _buffer.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>Adds a float to the packet.</summary>
        /// <param name="value">The float to add.</param>
        public void Write(float value)
        {
            _buffer.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>Adds a bool to the packet.</summary>
        /// <param name="value">The bool to add.</param>
        public void Write(bool value)
        {
            _buffer.AddRange(BitConverter.GetBytes(value));
        }

        /// <summary>Adds a string to the packet.</summary>
        /// <param name="value">The string to add.</param>
        public void Write(string value)
        {
            Write(value.Length); // RegisterForData the length of the string to the packet
            _buffer.AddRange(Encoding.ASCII.GetBytes(value)); // RegisterForData the string itself
        }

        #endregion

        #region Read Data

        /// <summary>Reads a byte from the packet.</summary>
        /// <param name="moveReadPos">Whether or not to move the buffer's read position.</param>
        public byte ReadByte(bool moveReadPos = true)
        {
            if (_buffer.Count > _readPos)
            {
                // If there are unread bytes
                var value = _readableBuffer[_readPos]; // Get the byte at readPos' position
                if (moveReadPos)
                    // If _moveReadPos is true
                    _readPos += 1; // Increase readPos by 1
                return value; // Return the byte
            }

            throw new Exception("Could not read value of type 'byte'!");
        }

        /// <summary>Reads an array of bytes from the packet.</summary>
        /// <param name="length">The length of the byte array.</param>
        /// <param name="moveReadPos">Whether or not to move the buffer's read position.</param>
        public byte[] ReadBytes(int length, bool moveReadPos = true)
        {
            if (_buffer.Count > _readPos)
            {
                // If there are unread bytes
                var value =
                    _buffer.GetRange(_readPos, length)
                        .ToArray(); // Get the bytes at readPos' position with a range of _length
                if (moveReadPos)
                    // If _moveReadPos is true
                    _readPos += length; // Increase readPos by _length
                return value; // Return the bytes
            }

            throw new Exception("Could not read value of type 'byte[]'!");
        }

        /// <summary>Reads a short from the packet.</summary>
        /// <param name="moveReadPos">Whether or not to move the buffer's read position.</param>
        public short ReadShort(bool moveReadPos = true)
        {
            if (_buffer.Count > _readPos)
            {
                // If there are unread bytes
                var value = BitConverter.ToInt16(_readableBuffer, _readPos); // Convert the bytes to a short
                if (moveReadPos)
                    // If _moveReadPos is true and there are unread bytes
                    _readPos += 2; // Increase readPos by 2
                return value; // Return the short
            }

            throw new Exception("Could not read value of type 'short'!");
        }

        /// <summary>Reads an int from the packet.</summary>
        /// <param name="moveReadPos">Whether or not to move the buffer's read position.</param>
        public int ReadInt(bool moveReadPos = true)
        {
            if (_buffer.Count > _readPos)
            {
                // If there are unread bytes
                var value = BitConverter.ToInt32(_readableBuffer, _readPos); // Convert the bytes to an int
                if (moveReadPos)
                    // If _moveReadPos is true
                    _readPos += 4; // Increase readPos by 4
                return value; // Return the int
            }

            throw new Exception("Could not read value of type 'int'!");
        }

        /// <summary>Reads a long from the packet.</summary>
        /// <param name="moveReadPos">Whether or not to move the buffer's read position.</param>
        public long ReadLong(bool moveReadPos = true)
        {
            if (_buffer.Count > _readPos)
            {
                // If there are unread bytes
                var value = BitConverter.ToInt64(_readableBuffer, _readPos); // Convert the bytes to a long
                if (moveReadPos)
                    // If _moveReadPos is true
                    _readPos += 8; // Increase readPos by 8
                return value; // Return the long
            }

            throw new Exception("Could not read value of type 'long'!");
        }

        /// <summary>Reads a float from the packet.</summary>
        /// <param name="moveReadPos">Whether or not to move the buffer's read position.</param>
        public float ReadFloat(bool moveReadPos = true)
        {
            if (_buffer.Count > _readPos)
            {
                // If there are unread bytes
                var value = BitConverter.ToSingle(_readableBuffer, _readPos); // Convert the bytes to a float
                if (moveReadPos)
                    // If _moveReadPos is true
                    _readPos += 4; // Increase readPos by 4
                return value; // Return the float
            }

            throw new Exception("Could not read value of type 'float'!");
        }

        /// <summary>Reads a bool from the packet.</summary>
        /// <param name="moveReadPos">Whether or not to move the buffer's read position.</param>
        public bool ReadBool(bool moveReadPos = true)
        {
            if (_buffer.Count > _readPos)
            {
                // If there are unread bytes
                var value = BitConverter.ToBoolean(_readableBuffer, _readPos); // Convert the bytes to a bool
                if (moveReadPos)
                    // If _moveReadPos is true
                    _readPos += 1; // Increase readPos by 1
                return value; // Return the bool
            }

            throw new Exception("Could not read value of type 'bool'!");
        }

        /// <summary>Reads a string from the packet.</summary>
        /// <param name="moveReadPos">Whether or not to move the buffer's read position.</param>
        public string ReadString(bool moveReadPos = true)
        {
            try
            {
                var length = ReadInt(moveReadPos); // Get the length of the string
                var value =
                    Encoding.ASCII.GetString(_readableBuffer, moveReadPos ? _readPos : _readPos + 4,
                        length); // Convert the bytes to a string
                if (moveReadPos && value.Length > 0)
                    // If _moveReadPos is true string is not empty
                    _readPos += length; // Increase readPos by the length of the string
                return value; // Return the string
            }
            catch
            {
                throw new Exception("Could not read value of type 'string'!");
            }
        }

        public byte[] ToByteArray()
        {
            return _buffer.ToArray();
        }

        #endregion
    }
}