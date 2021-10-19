using System;
using System.Collections.Generic;

namespace Facade_and_Composite
{

    interface IGraphic
    {
        IGraphic Move(int x, int y);
        IGraphic Draw();
    }

    class Dot : IGraphic
    {

        private int _x { get; set; }
        private int _y { get; set; }

        public Dot(int x, int y)
        {
            _x = x;
            _y = y;
        }

        virtual public IGraphic Draw()
        {
            Console.WriteLine("Dot  drawed");
            return this;
        }

        virtual public IGraphic Move(int x, int y)
        {
            _x = x;
            _y = y;
            return this;
        }
    }

    class Circle : Dot
    {
        public double Radius { get; set; }
        public Circle(int x, int y, int Radius) : base(x, y)
        {
            this.Radius = Radius;
        }
        public override IGraphic Draw()
        {
            Console.WriteLine($"Circle drawed");
            return this;
        }
    }

    class CompoundGraphic : IGraphic
    {
        public List<IGraphic> childrens = new List<IGraphic>();

        public IGraphic Add(IGraphic child)
        {
            childrens.Add(child);
            return this;
        }
        public IGraphic Remove(IGraphic child)
        {
            childrens.Remove(child);
            return this;
        }
        public IGraphic Move(int x, int y)
        {
            foreach (var children in childrens) children.Move(x, y);
            return this;
        }
        public IGraphic Draw()
        {
            foreach (var children in childrens) children.Draw();
            return this;
        }


    }


    class ImageEditor
    {
        public CompoundGraphic compoundGraphic { get; set; }

        public void Load()
        {
            compoundGraphic = new CompoundGraphic();
            compoundGraphic.Add(new Circle(10,9,14));
            compoundGraphic.Add(new Dot(1, 2));
        }


        public void GroupSelected(List<IGraphic> graphics)
        {
            var group = new CompoundGraphic();
            for (int i = 0; i < graphics.Count; i++)
            {
                group.Add(graphics[i]);
                compoundGraphic.Remove(graphics[i]);
            }
            compoundGraphic.Add(group);
            compoundGraphic.Draw();
        }

    }

    public class Program
    {
        static void Main(string[] args)
        {
            ImageEditor image = new ImageEditor();
            image.Load();

            image.GroupSelected(image.compoundGraphic.childrens);
        }
    }
}
