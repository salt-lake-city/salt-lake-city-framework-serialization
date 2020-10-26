using System;
using System.Collections.Generic;
using System.Reflection;

namespace SaltLakeCity.Framework.Serialization
{
    /// <summary>
    /// Automatischer Serializer, der über Reflection serialisiert
    /// <remarks>Sollte nur an Stellen verwendet werden, die nicht Performancekritisch sind</remarks>
    /// </summary>
    /// <typeparam name="TFor">Typ, für den der Serializer ist</typeparam>
    public class AutoSerializer<TFor> : SerializerBase<TFor>
    {
        /// <summary>
        /// Delegate für das Schreiben eines Wertes
        /// </summary>
        /// <param name="value">Wert, der geschrieben werden soll</param>
        /// <param name="serializer">Serializer, mit dem Serialisiert wird</param>
        private delegate void WriteDelegate(object value, ByteSerializer serializer);

        private delegate object ReadDelegate(ByteSerializer serializer);

        private Dictionary<Type, WriteDelegate> _writeDelegates = new Dictionary<Type, WriteDelegate>()
        {
            {
                typeof(string),
                (value, serializer) => serializer.Write((string) value)
            },
            {
                typeof(int),
                (value, serializer) => serializer.Write((int) value)
            },  
            {
                typeof(bool),
                (value, serializer) => serializer.Write((bool) value)
            },
            {
                typeof(float),
                (value, serializer) => serializer.Write((float) value)
            }
        };
        
        private Dictionary<Type, ReadDelegate> _readDelegates = new Dictionary<Type, ReadDelegate>()
        {
            {
                typeof(string),
                serializer => serializer.ReadString()
            },
            {
                typeof(int),
                serializer => serializer.ReadInt()
            },   
            {
                typeof(bool),
                serializer => serializer.ReadBool()
            },
            {
                typeof(float),
                serializer => serializer.ReadFloat()
            }
        };
        
        /// <summary>
        /// Properties der zu Serialisierenden Klasse
        /// </summary>
        private PropertyInfo[] _properties;

        public AutoSerializer()
        {
            _properties = typeof(TFor).GetProperties();
        }

        /// <summary>
        /// Serialisiert das Objekt in den Byte Serializer
        /// </summary>
        /// <param name="value">Wert, der Serialisiert werden soll</param>
        /// <param name="serializer">Serializer, in den die Werte geschrieben werden</param>
        protected override void Serialize(TFor value, ByteSerializer serializer)
        {
            foreach (var propertyInfo in _properties)
            {
                _writeDelegates[propertyInfo.PropertyType](propertyInfo.GetValue(value), serializer);
            }
        }

        /// <summary>
        /// Deserialisiert ein Objekt
        /// </summary>
        /// <param name="serializer">Serializer, dem das serialisierte Objekt zu entnehmen ist</param>
        /// <returns>Deserialisierte Instanz</returns>
        protected override TFor Deserialize(ByteSerializer serializer)
        {
            // => Instanz erzeugen
            var instance = (TFor) Activator.CreateInstance(typeof(TFor));

            // => Deserialisieren
            foreach (var propertyInfo in _properties)
            {
                var value = _readDelegates[propertyInfo.PropertyType](serializer);
                propertyInfo.SetValue(instance, value);
            }
            
            return instance;
        }
    }
}