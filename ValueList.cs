using System;

public struct ValueList<T> where T : struct
{
    private const int DefaultCapacity = 4;

    private T[] _items;

    public int Count { get; private set; }
    public int Capacity => _items.Length;

    public T this[ int key ]
    {
        get => GetRef( key );

        set => GetRef( key ) = value;
    }

    public ref T GetRef( int key )
    {
        if ( key >= Count )
        {
            throw new IndexOutOfRangeException( nameof( key ) );
        }
        
        return ref _items[ key ];
    }

    public void Allocate( int capacity = DefaultCapacity )
    {
        _items = capacity > 0 ? new T[ capacity ] : Array.Empty<T>();

        Clear();
    }

    public void Add( T newItem )
    {
        if ( Count == Capacity )
        {
            Grow();
        } 

        _items[ Count++ ] = newItem;
    }

    public void Grow()
    {
        T[] newArray = new T[ Capacity << 1 ];
        
        Array.Copy( _items, 0, newArray, 0, Count );

        _items = newArray;
    }

    public void Remove( int key )
    {
        GetRef( key ) = _items[ --Count ];
    }

    public void Clear()
    {
        Count = 0;
    }
}
