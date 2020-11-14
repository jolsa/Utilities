using System;

namespace RecentFiles
{
	public class EventManager
	{
		private Action<EventHandler> Add { get; }
		private Action<EventHandler> Remove { get; }
		private EventHandler Handler { get; }

		public EventManager(Action<EventHandler> add, Action<EventHandler> remove, EventHandler handler)
		{
			Add = add;
			Remove = remove;
			Handler = handler;
		}

		public void Attach() { Add(Handler); }

		public void Detach() { Remove(Handler); }
	}
}
