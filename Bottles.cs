using System.Linq;
using System;

namespace BottlesProject
{
    public class Bottles
    {
        public string Song()
        {
            return Verses(99, 0);
        }

        public string Verses(int upper, int lower)
        {
            return string.Join(
                Environment.NewLine,
                Enumerable.Range(lower, upper-lower+1)
                .Reverse()
                .Select(num => Verse(num)));
        }

        public string Verse(int number)
        {
            var bottleNumber = BottleNumber.For(number);
            var nextBottleNumber = bottleNumber.Successor;

            return
                $"{bottleNumber.Description.Capitalize()} of beer on the wall, " +
                $"{bottleNumber.Description} of beer.\r\n" +
                $"{bottleNumber.Action}, " +
                $"{nextBottleNumber.Description} of beer on the wall.\r\n";
        }
    }

    public class BottleNumber
    {
        readonly int _number;

        public BottleNumber(int number)
        {
            _number = number;
        }

        public static BottleNumber For(int number)
        {
            switch (number)
            {
                case 0:
                    return new BottleNumber0(number);

                case 1:
                    return new BottleNumber1(number);

                case 6:
                    return new BottleNumber6(number);

                default:
                    return new BottleNumber(number);
            }
        }

        public virtual string Description => $"{Quantity} {Container}";

        public  virtual string Action => $"Take {Pronoun} down and pass it around";

        public virtual BottleNumber Successor => BottleNumber.For(_number-1);

        protected virtual string Quantity => _number.ToString();

        protected virtual string Container => "bottles";

        protected virtual string Pronoun => "one";
    }

    public class BottleNumber0 : BottleNumber
    {
        public BottleNumber0(int number) : base(number) {}

        public override string Action => "Go to the store and buy some more";

        public override BottleNumber Successor => BottleNumber.For(99);

        protected override string Quantity => "no more";
    }

    public class BottleNumber1 : BottleNumber
    {
        public BottleNumber1(int number) : base(number) {}

        protected override string Container => "bottle";

        protected override string Pronoun => "it";
    }

    public class BottleNumber6 : BottleNumber
    {
        public BottleNumber6(int number) : base(number) {}
   
        protected override string Container => "six-pack";

        protected override string Quantity => "1";
    }

    public static class Extensions
    {
        public static string Capitalize(this string str)
        {
            if (string.IsNullOrEmpty(str)) { return str; }

            var strArray = str.ToCharArray();
            strArray[0] = char.ToUpper(strArray[0]);
            return new string(strArray);
        }
    }
}
