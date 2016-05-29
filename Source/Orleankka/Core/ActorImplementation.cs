using System;
using System.Threading.Tasks;

namespace Orleankka.Core
{
    class ActorImplementation
    {
        public static readonly ActorImplementation Undefined = new ActorImplementation();

        public static ActorImplementation From(Type actor) => 
            new ActorImplementation(actor);

        readonly GC gc;
        readonly Dispatcher dispatcher;

        ActorImplementation()
        {}

        ActorImplementation(Type type)
        {
            Type = type;
            gc = new GC(type);
            dispatcher = new Dispatcher(type);
        }

        internal Type Type { get; private set; }

        internal void KeepAlive(ActorEndpoint endpoint) => 
            gc.KeepAlive(endpoint);

        internal Task<object> Dispatch(Actor target, object message, Func<object, Task<object>> fallback) => 
            dispatcher.Dispatch(target, message, fallback);

        internal bool DeclaresHandlerFor(Type message)=> 
            dispatcher.HasRegisteredHandler(message);
    }
}