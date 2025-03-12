using HireMeBackEnd.Models.Client;
using System.Reflection;

namespace HireMeBackEnd.Utilities.Client
{
    public abstract class ClientBase : IConvertible
    {
        public abstract void Configure(IServiceProvider serviceProvider);

        protected List<Type> provides = new List<Type>();
        public void Provides(IEnumerable<Type> provides)
        {
            this.provides = provides.ToList();
        }
        protected List<Type> consumes = new List<Type>();
        public void Consumes(IEnumerable<Type> consumes)
        {
            this.consumes = consumes.ToList();
        }

        public void ProvideAllProducts()
        {
            // TODO: Check resource intensity
            foreach (var type in provides)
            {
                Type interfaceType = typeof(ISource<>).MakeGenericType(type);
                if (interfaceType.IsAssignableFrom(GetType()))
                {
                    var method = interfaceType.GetMethod("SourceProductDeltas");

                    if (method != null)
                    {
                        var interfaceInstance = Convert.ChangeType(this, interfaceType);
                        var result = method.Invoke(interfaceInstance, null);

                        // Get the EventInfo for the event
                        var eventInfo = interfaceType.GetEvent("OnProductDelta");
                        if (eventInfo != null)
                        {
                            var eventDelegate = (MulticastDelegate)interfaceType
                                .GetField("OnProductDelta", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                                ?.GetValue(interfaceInstance);
                        }
                    }
                }
            }
        }

        public TypeCode GetTypeCode() => TypeCode.Object;
        public object ToType(Type conversionType, IFormatProvider? provider)
        {
            if (conversionType.IsAssignableFrom(GetType())) return this;
            throw new InvalidCastException($"Cannot convert {GetType().Name} to {conversionType.Name}");
        }

        public bool ToBoolean(IFormatProvider? provider) => throw new NotSupportedException();
        public byte ToByte(IFormatProvider? provider) => throw new NotSupportedException();
        public char ToChar(IFormatProvider? provider) => throw new NotSupportedException();
        public DateTime ToDateTime(IFormatProvider? provider) => throw new NotSupportedException();
        public decimal ToDecimal(IFormatProvider? provider) => throw new NotSupportedException();
        public double ToDouble(IFormatProvider? provider) => throw new NotSupportedException();
        public short ToInt16(IFormatProvider? provider) => throw new NotSupportedException();
        public int ToInt32(IFormatProvider? provider) => throw new NotSupportedException();
        public long ToInt64(IFormatProvider? provider) => throw new NotSupportedException();
        public sbyte ToSByte(IFormatProvider? provider) => throw new NotSupportedException();
        public float ToSingle(IFormatProvider? provider) => throw new NotSupportedException();
        public string ToString(IFormatProvider? provider) => throw new NotSupportedException();
        public ushort ToUInt16(IFormatProvider? provider) => throw new NotSupportedException();
        public uint ToUInt32(IFormatProvider? provider) => throw new NotSupportedException();
        public ulong ToUInt64(IFormatProvider? provider) => throw new NotSupportedException();
    }
}
