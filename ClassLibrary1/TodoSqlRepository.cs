using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary1
{
    public class TodoSqlRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;

        public TodoSqlRepository(TodoDbContext context)
        {
            _context = context;
        }


        static void Main(string[] args)
        {
            
        }

        public void Add(TodoItem todoItem)
        {
            if (!_context._items.Find(todoItem.Id).Equals(null))
            {
                _context._items.Add(todoItem);
                _context.SaveChanges();
            }
            else
            {
                throw new DuplicateTodoItemException( " duplicate id: " + todoItem.Id);
            }

        }

        public TodoItem Get(Guid todoId, Guid userId)
        {
            TodoItem item = _context._items.Find(todoId);
            if (!item.UserId.Equals(userId))
            {
                throw new TodoAccessDeniedException("user is not the owner of the Todo item");
            }
            else
            {
                return item;
            }
        }

        public List<TodoItem> GetActive(Guid userId)
        {
            List<TodoItem> items = _context._items.Where(s => !s.IsCompleted.Equals(true) && s.UserId.Equals(userId)).ToList();
            return items;
        }

        public List<TodoItem> GetAll(Guid userId)
        {
            List<TodoItem> items = _context._items.Where(s=>s.UserId.Equals(userId)).ToList();
            return items;
        }

        public List<TodoItem> GetCompleted(Guid userId)
        {
            List<TodoItem> items = _context._items.Where(s=>s.IsCompleted.Equals(true) && s.UserId.Equals(userId)).ToList();
            return items;
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction, Guid userId)
        {
            List<TodoItem> items = _context._items.Where(filterFunction).Where(s=> s.UserId.Equals(userId)).ToList();
            return items;
        }

        public bool MarkAsCompleted(Guid todoId, Guid userId)
        {
            TodoItem item = Get(todoId, userId);
            if (item != null)
            {
                item.IsCompleted = true;
                item.DateCompleted = DateTime.Now;
                Update(item, userId);

                return true;
            }

            return false;
        }

        public bool Remove(Guid todoId, Guid userId)
        {
            TodoItem item = Get(todoId, userId);
            if (item != null)
            {
                _context._items.Remove(item);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
           
        }

        public void Update(TodoItem todoItem, Guid userId)
        {
            TodoItem item = Get(todoItem.Id, userId);
            if (item != null)
            {
                _context._items.Remove(item);                
            }

            _context._items.Add(todoItem);
            _context.SaveChanges();

        }
    }
}
