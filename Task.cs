using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.MapObjects
{
    interface ICanBeat
    {
        Army Army { get; set; }
    }

    interface IConsume
    {
        Treasure Treasure { get; set; }
    }

    interface IKeeper
    {
        int Owner { get; set; }
    }

    public class Dwelling : IKeeper
    {
        public int Owner { get; set; }
    }

    public class Mine : IKeeper, ICanBeat, IConsume
    {
        public int Owner { get; set; }
        public Army Army { get; set; }
        public Treasure Treasure { get; set; }
    }

    public class Creeps : ICanBeat, IConsume
    {
        public Army Army { get; set; }
        public Treasure Treasure { get; set; }
    }

    public class Wolfs : ICanBeat
    {
        public Army Army { get; set; }
    }

    public class ResourcePile : IConsume
    {
        public Treasure Treasure { get; set; }
    }

    public static class Interaction
    {
        public static void Make(Player player, object mapObject)
        {
            if (mapObject is ICanBeat)
                if (player.CanBeat((mapObject as ICanBeat).Army))
                {
                    if (mapObject is IKeeper) (mapObject as IKeeper).Owner = player.Id;
                    if (mapObject is IConsume) player.Consume((mapObject as IConsume).Treasure);
                    return;
                }
                else
                {
                    player.Die();
                    return;
                }
            if (mapObject is IKeeper) (mapObject as IKeeper).Owner = player.Id;
            if (mapObject is IConsume) player.Consume((mapObject as IConsume).Treasure);
        }
    }
}
