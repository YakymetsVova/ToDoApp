using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TodoApplication.Services;

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
        private readonly string PATH = $"{Environment.CurrentDirectory}\\todoDataList.json";
        public LimitedSizeStack<ActionStack<TItem>> UserActions;
        private FileIOService<TItem> fileIOService;
        public List<TItem> Items { get; }
        public int Limit;
        public ListModel(int limit)
        {
            fileIOService = new FileIOService<TItem>(PATH);
            try
            {
                Items = fileIOService.LoadData();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
            Limit = limit;
            UserActions = new LimitedSizeStack<ActionStack<TItem>>(limit);
        }


        public void AddItem(TItem item)
        {
            if (item.ToString() == "") return;
            UserActions.Push(new ActionStack<TItem>(item, Items.Count, Command.Add));
            Items.Add(item);
            fileIOService.SaveData(Items);
        }

        public void RemoveItem(int index)
        {
            UserActions.Push(new ActionStack<TItem>(Items[index], index, Command.Remove));
            Items.RemoveAt(index);
            fileIOService.SaveData(Items);
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
            fileIOService.SaveData(Items);
        }
    }
}
