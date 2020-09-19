namespace SaltLakeCity.Framework.Serialization
{
    /// <summary>
    ///     Serializer, der ein Objekt De-/Serialisieren kann
    /// </summary>
    public interface ISerializer<TFor>
    {
        /// <summary>
        ///     Serialisiert das Objekt
        /// </summary>
        /// <param name="value">Instanz, die serialisiert werden soll</param>
        /// <returns>Serialisierte Instanz in Form eines byte-Arrays</returns>
        byte[] Serialize(TFor value);

        /// <summary>
        ///     Deserialisiert das Objekt
        /// </summary>
        /// <param name="from">Byte-Array, aus dem die Instanz erzeugt wird</param>
        /// <returns>Deserialisierte Instanz</returns>
        TFor Deserialize(byte[] from);
    }
}