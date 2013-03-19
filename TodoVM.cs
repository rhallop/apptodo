using Microsoft.Phone.Scheduler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TodoApp
{
    public class TodoVM
    {
        public ObservableCollection<Todo> listTodo = new ObservableCollection<Todo>();
        
        
        
        public void SaveTodo(Todo _todo)
        {
            
            try
            {
                int last =1;
                if (listTodo.Count() > 0)
                {
                  last = listTodo.Max(x => x.Id);
                }
                _todo.Id = last + 1;              
                listTodo.Add(_todo);

                Reminder rem = new Reminder(_todo.Title);
                rem.Content = _todo.Content;
                rem.BeginTime = _todo.Deadline;
                ScheduledActionService.Add(rem);
            }
            catch 
            {
                //MessageBox.Show(e.Message);
            }
        }

        public Todo findTodoById(int _id)
        {
            Todo todo = listTodo.Single(x => x.Id == _id);

            return todo;
        }

        public int countByDeadlines(DateTime dtime)
        {
            int count = listTodo.Count(x => x.Deadline.Date == dtime.Date && x.IsDone == false);
            return count;

        }

        public ObservableCollection<Todo> todosNotDone()
        {
            ObservableCollection<Todo> temp = new ObservableCollection<Todo>();
            foreach (var t in listTodo)
            {
                if (t.IsDone == false)
                {
                    temp.Add(t);
                }
            }

            return temp;
        }

        public void markTodoAsDone(int id)
        {
            findTodoById(id).IsDone = true;
            ScheduledActionService.Remove(findTodoById(id).Title);

        }
    }
}
