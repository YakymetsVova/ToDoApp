using System;
using System.Collections.Generic;

namespace TodoApplication
{
    public class ActionStack<TItem>
    {
        public int Index { get; set; }
        public TItem Value { get; set; }
        public Command Action { get; set; }

        public ActionStack(TItem value, int index, Command action)
        {
            Value = value;
            Index = index;
            Action = action;
        }
    }

    public enum Command
    {
        Remove,
        Add
    }

    public class ListModel<TItem>
    {
        public LimitedSizeStack<ActionStack<TItem>> UserActions;
        public List<TItem> Items { get; }
        public int Limit;
        public ListModel(int limit)
        {
            Items = new List<TItem>();
            Limit = limit;
            UserActions = new LimitedSizeStack<ActionStack<TItem>>(limit);
        }


        public void AddItem(TItem item)
        {
            if (item.ToString() == "") return;
            UserActions.Push(new ActionStack<TItem>(item, Items.Count, Command.Add));
            Items.Add(item);
        }

        public void RemoveItem(int index)
        {
            UserActions.Push(new ActionStack<TItem>(Items[index], index, Command.Remove));
            Items.RemoveAt(index);
        }

        public bool CanUndo()
        {
            return UserActions.Count > 0;
        }

        public void Undo()
        {
            var lastUserAction = UserActions.Pop();
            if (lastUserAction.Action == Command.Remove)
                Items.Insert(lastUserAction.Index, lastUserAction.Value);
            else if (lastUserAction.Action == Command.Add)
                Items.RemoveAt(lastUserAction.Index);
        }
    }
}
