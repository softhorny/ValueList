using System;

namespace softh.Collections
{
    public struct ValueList<TItem> where TItem : struct
    {
        private const int DefaultCapacity = 4;

        private TItem[] _items;

        public int Count { get; private set; }

        public int Capacity => _items.Length;

        public TItem this[ Index index ]
        {
            get => _items[ index.GetOffset( Count ) ];

            set => _items[ index.GetOffset( Count ) ] = value;
        }

        public void Allocate( int capacity = DefaultCapacity )
        {
            _items = capacity > 0 ? new TItem[ capacity ] : Array.Empty<TItem>();

            Clear();
        }

        public void Add( TItem newItem )
        {
            if ( Count == Capacity )
            {
                Grow();
            } 

            _items[ Count++ ] = newItem;
        }

        public void Grow()
        {
            TItem[] newArray = new TItem[ Capacity << 1 ];

            Array.Copy( _items, 0, newArray, 0, Count );

            _items = newArray;
        }

        public void Remove( Index index )
        {
            _items[ index.GetOffset( Count ) ] = _items[ --Count ];
        }

        public void Clear()
        {
            Count = 0;
        }
    }
}
