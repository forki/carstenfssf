// The verbs of the system (in imperfect form)
module Events

    open System

    (*
    #r "System.CoreEx.dll"
    #r "System.Reactive.dll" 
    *)

    // Events implemented as discriminated union. 
    // If you use a big solution, change to a base type 
    // or just use many event storages and concatenate / merge them with LINQ 
    type Event = 
    | InventoryItemCreated      of Guid * string
    | InventoryItemDeactivated  of Guid
    | InventoryItemRenamed      of Guid * string
    | ItemsCheckedInToInventory of Guid * int
    | ItemsRemovedFromInventory of Guid * int
    with 
        override x.ToString() = 
            match x with
            | InventoryItemCreated(i,n) -> "Item " + n + " created (id:" + i.ToString() + ")"
            | InventoryItemDeactivated(i) -> "Item deactivated (id:" + i.ToString() + ")"
            | InventoryItemRenamed(i,n) -> "Item renamed to " + n + " created (id:" + i.ToString() + ")"
            | ItemsCheckedInToInventory(i,c) -> "Check-in " + c.ToString() + " of item (id:" + i.ToString() + ")"
            | ItemsRemovedFromInventory(i,c) -> "Removed " + c.ToString() + " of item (id:" + i.ToString() + ")"


//            public class WeatherSubscriber : IObservable<WeatherData>
//    {
//        private List<IObserver<WeatherData>> observers;
//        public WeatherSubscriber()
//        {
//            observers = new List<IObserver<WeatherData>>();
//        }
//        public IDisposable Subscribe(IObserver<WeatherData> observer)
//        {
//            if (!observers.Contains(observer))
//                observers.Add(observer);
//            return new Unsubscriber(observers, observer);
//        }
//        private class Unsubscriber : IDisposable
//        {
//            private List<IObserver<WeatherData>> _observers;
//            private IObserver<WeatherData> _observer;
//            public Unsubscriber(List<IObserver<WeatherData>> observers, IObserver<WeatherData> observer)
//            {
//                this._observers = observers;
//                this._observer = observer;
//            }
//            public void Dispose()
//            {
//                if (_observer != null && _observers.Contains(_observer))
//                    _observers.Remove(_observer);
//            }
//        } 

    type ObserverRec<'a> = 
        { Observer:IObserver<'a> }
        with 
            member this.Notify(item:'a) =
                this.Observer.OnNext item
            member this.Disopse() =
                { new IDisposable with
                    member x.Dispose() = 
                        () }

  
       // let subscribers =new System.Collections.Concurrent.ConcurrentDictionary<int, int>()

    let subscribe (ob:System.IObserver<'a>) = 
                
                
                ()