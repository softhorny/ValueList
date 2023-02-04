using System;

public struct ValueList<T> where T : struct
{
    private const int DefaultCapacity = 4;
    
    private T[] _items;

    public int Count { get; private set; }
    public int Capacity => _items.Length;

    public void Allocate( int capacity = DefaultCapacity )
    {
        _items = capacity > 0 ? new T[ capacity ] : Array.Empty<T>();
    }

    public T this[ int index ]
    {
        get => _items[ index ];
        set => _items[ index ] = value;
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
        int newLength = Capacity << 1;

        T[] newArray = new T[ newLength ];
        
        Array.Copy( _items, 0, newArray, 0, Count );

        _items = newArray;
    }

    public void Remove( int index )
    {
        _items[ index ] = _items[ --Count ];
    }
}
