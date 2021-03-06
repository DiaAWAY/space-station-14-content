using Content.Server.Players;
using SS14.Server.Interfaces.Console;
using SS14.Server.Interfaces.Player;
using SS14.Shared.Interfaces.GameObjects;
using SS14.Shared.IoC;

namespace Content.Server.Administration
{
    public class AGhost : IClientCommand
    {
        public string Command => "aghost";
        public string Description => "Makes you an admin ghost.";
        public string Help => "aghost";

        public void Execute(IConsoleShell shell, IPlayerSession player, string[] args)
        {
            if (player == null)
            {
                shell.SendText((IPlayerSession) null, "Nah");
                return;
            }

            var mind = player.ContentData().Mind;
            if (mind.VisitingEntity != null && mind.VisitingEntity.Prototype.ID == "AdminObserver")
            {
                var visiting = mind.VisitingEntity;
                mind.UnVisit();
                visiting.Delete();
            }
            else
            {
                var entityManager = IoCManager.Resolve<IEntityManager>();
                var ghost = entityManager.ForceSpawnEntityAt("AdminObserver",
                    player.AttachedEntity.Transform.GridPosition);

                mind.Visit(ghost);
            }
        }
    }
}
