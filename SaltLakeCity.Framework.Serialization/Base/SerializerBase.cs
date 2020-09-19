namespace SaltLakeCity.Framework.Serialization
{
    /// <inheritdoc />
    public abstract class SerializerBase<TFor> : ISerializer<TFor>
    {
        /// <summary>
        ///     Serialisiert das Objekt
        /// </summary>
        /// <param name="value">Instanz, die serialisiert werden soll</param>
        /// <returns>Serialisierte Instanz in Form eines byte-Arrays</returns>
        public byte[] Serialize(TFor value)
        {
            var byteSerializer = new ByteSerializer();
            Serialize(value, byteSerializer);
            return byteSerializer.ToByteArray();
        }

        /// <summary>
        ///     Deserialisiert das Objekt
        /// </summary>
        /// <param name="from">Byte-Array, aus dem die Instanz erzeugt wird</param>
        /// <returns>Deserialisierte Instanz</returns>
        public TFor Deserialize(byte[] from)
        {
            var byteSerializer = new ByteSerializer(from);
            return Deserialize(byteSerializer);
        }

        /// <summary>
        /// Serialisiert das Objekt in den Byte Serializer
        /// </summary>
        /// <param name="value">Wert, der Serialisiert werden soll</param>
        /// <param name="serializer">Serializer, in den die Werte geschrieben werden</param>
        protected abstract void Serialize(TFor value, ByteSerializer serializer);

        /// <summary>
        /// Deserialisiert ein Objekt
        /// </summary>
        /// <param name="serializer">Serializer, dem das serialisierte Objekt zu entnehmen ist</param>
        /// <returns>Deserialisierte Instanz</returns>
        protected abstract TFor Deserialize(ByteSerializer serializer);
    }
}