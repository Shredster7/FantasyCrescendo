using UnityEngine;

namespace HouraiTeahouse.SmashBrew {

    public interface IDamageable {

        void Damage(object source, float damage);

    }

    public interface IHealable {

        void Heal(object source, float healing);

    }

    public interface IKnockbackable {

        void Knockback(object source, Vector2 knockback);

    }

    public interface IAbsorbable {
        void Absorb(object source);
    }

    public interface IReflectable {
        void Reflect(object source);
    }
}
