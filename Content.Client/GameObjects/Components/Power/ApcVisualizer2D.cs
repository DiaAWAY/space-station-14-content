using Content.Shared.GameObjects.Components.Power;
using SS14.Client.GameObjects;
using SS14.Client.Interfaces.GameObjects.Components;
using SS14.Shared.Interfaces.GameObjects;

namespace Content.Client.GameObjects.Components.Power
{
    public class ApcVisualizer2D : AppearanceVisualizer
    {
        public override void InitializeEntity(IEntity entity)
        {
            base.InitializeEntity(entity);

            var sprite = entity.GetComponent<ISpriteComponent>();

            sprite.LayerMapSet(Layers.ChargeState, sprite.AddLayerState("apco3-0"));
            sprite.LayerSetShader(Layers.ChargeState, "unshaded");

            sprite.LayerMapSet(Layers.Lock, sprite.AddLayerState("apcox-0"));
            sprite.LayerSetShader(Layers.Lock, "unshaded");

            sprite.LayerMapSet(Layers.Equipment, sprite.AddLayerState("apco0-3"));
            sprite.LayerSetShader(Layers.Equipment, "unshaded");

            sprite.LayerMapSet(Layers.Lighting, sprite.AddLayerState("apco1-3"));
            sprite.LayerSetShader(Layers.Lighting, "unshaded");

            sprite.LayerMapSet(Layers.Environment, sprite.AddLayerState("apco2-3"));
            sprite.LayerSetShader(Layers.Environment, "unshaded");
        }

        public override void OnChangeData(AppearanceComponent component)
        {
            base.OnChangeData(component);

            var sprite = component.Owner.GetComponent<ISpriteComponent>();
            if (component.TryGetData<ApcChargeState>(ApcVisuals.ChargeState, out var state))
            {
                switch (state)
                {
                    case ApcChargeState.Lack:
                        sprite.LayerSetState(Layers.ChargeState, "apco3-0");
                        break;
                    case ApcChargeState.Charging:
                        sprite.LayerSetState(Layers.ChargeState, "apco3-1");
                        break;
                    case ApcChargeState.Full:
                        sprite.LayerSetState(Layers.ChargeState, "apco3-2");
                        break;
                }
            }
            else
            {
                sprite.LayerSetState(Layers.ChargeState, "apco3-0");
            }
        }

        enum Layers
        {
            ChargeState,
            Lock,
            Equipment,
            Lighting,
            Environment,
        }
    }
}
