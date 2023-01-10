using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2017.Day20;

class Particles {
    public int i;
    public Points position;
    public Points velocity;
    public Points acceleration;

    public bool destroyed = false;
 

    public Particles (int i, Points position, Points velocity, Points acceleration) {
        this.i = i;
        this.position = position;
        this.velocity = velocity;
        this.acceleration = acceleration;
    }

    public void ZBuffering() {
        (velocity.x, velocity.y, velocity.z) = (velocity.x + acceleration.x, velocity.y + acceleration.y, velocity.z + acceleration.z);
        (position.x, position.y, position.z) = (position.x + velocity.x, position.y + velocity.y, position.z + velocity.z);
    }

    public IEnumerable<int> ParticlesCollide(Particles particle) {
        return
            from tickX in Collide(particle.acceleration.x - acceleration.x, particle.velocity.x - velocity.x, particle.position.x - position.x)
            from tickY in Collide(particle.acceleration.y - acceleration.y, particle.velocity.y - velocity.y, particle.position.y - position.y)
            from tickZ in Collide(particle.acceleration.z - acceleration.x, particle.velocity.z - velocity.z, particle.position.z - position.z)
            where tickX == tickY && tickY == tickZ
            select (tickX);
    }

    private IEnumerable<int> Collide(int acceleration, int velocity, int position) =>
        Solve(acceleration / 2, velocity, position);

    private IEnumerable<int> Solve(int acceleration, int velocity, int position) {
        if (acceleration == 0) {
            if (velocity == 0) {
                if (position == 0) {
                    yield return 0;
                }
            } else {
                yield return - position / velocity;
            }
        } else {
            var destroyed = velocity * velocity - 4 * acceleration * position;
            if (destroyed == 0) {
                yield return -velocity / (2 * acceleration);
            } else if (destroyed > 0) {
                var ds = Math.Sqrt(destroyed);
                if (ds * ds == destroyed) {
                    yield return (int)((-velocity + ds) / (2 * acceleration));
                    yield return (int)((-velocity - ds) / (2 * acceleration));
                }

            }
        }
    }
}
