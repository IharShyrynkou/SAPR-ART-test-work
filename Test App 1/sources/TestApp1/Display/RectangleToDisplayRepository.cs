using System;
using System.Linq;
using System.Collections.Generic;

namespace TestApp1.Display
{
    public class RectangleToDisplayRepository : IGenericRepository<RectangleToDisplay>
    {
        private Dictionary<Guid,RectangleToDisplay> _rectangles;

        public RectangleToDisplayRepository()
        {
            _rectangles = new Dictionary<Guid, RectangleToDisplay>();
        }

        public void Create(RectangleToDisplay item)
        {
            _rectangles.Add(item.Id, item);
        }

        public RectangleToDisplay FindById(Guid id)
        {
            return _rectangles[id];
        }

        public IEnumerable<RectangleToDisplay> Get()
        {
            return _rectangles.Values.ToList();
        }

        public IEnumerable<RectangleToDisplay> Get(Func<RectangleToDisplay, bool> predicate)
        {
            return _rectangles.Values.Where(predicate).ToList();
        }

        public void Remove(RectangleToDisplay item)
        {
            _rectangles.Remove(item.Id);
        }

        public void Update(RectangleToDisplay item)
        {
            _rectangles[item.Id] = item;
        }
    }
}