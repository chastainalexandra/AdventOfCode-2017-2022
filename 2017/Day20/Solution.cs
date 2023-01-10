using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2017.Day20;

[ProblemName("Particle Swarm")]
class Solution : Solver {

    // public string GetName() => "Particle Swarm"; have to move this to above with upgrade 

    // It transmits to you a buffer (your puzzle input) listing each particle in order (starting with particle 0, then particle 1, particle 2, and so on). 
    // For each particle, it provides the X, Y, and Z coordinates for the particle's position (p), velocity (v), and acceleration (a), each in the format <X,Y,Z>.
     List<Particles> ParticleParse(string fileData) {
        var lines = fileData.Split('\n');
        return (
             from q in Enumerable.Zip(lines, Enumerable.Range(0, int.MaxValue), (line, i) => (i: i, line: line))
             let nums = Regex.Matches(q.line, "-?[0-9]+").Select(m => int.Parse(m.Value)).ToArray()
             let position = new Points(nums[0], nums[1], nums[2])
             let velocity = new Points(nums[3], nums[4], nums[5])
             let acceleration = new Points(nums[6], nums[7], nums[8])
             select new Particles(q.i, position, velocity, acceleration))
         .ToList();
    }

    // How many particles are left after all collisions are resolved?
    public object PartTwo(string fileData) {
        var fd = ParticleParse(fileData);
        var collisionTimes = (
            from particle1 in fd
            from particle2 in fd
            where particle1.i != particle2.i
            from collisionTime in particle1.ParticlesCollide(particle2)
            select collisionTime
        ).ToArray();
        var T = collisionTimes.Max();

        var t = 0;
        while (t <= T) {
            var particlesByPos = (from particle in fd orderby particle.position.x, particle.position.y, particle.position.z select particle).ToArray();
            
            var particlePrev = particlesByPos[0];

            for (int i = 1; i < particlesByPos.Length; i++) {
                var particle = particlesByPos[i];
                if (particlePrev.position.x == particle.position.x && particlePrev.position.y == particle.position.y && particlePrev.position.z == particle.position.z) {
                    particlePrev.destroyed = true;
                    particle.destroyed = true;
                }
                particlePrev = particle;
            }

            if (fd.Any(p => p.destroyed)) {
                fd = fd.Where(particle => !particle.destroyed).ToList();
            }

            foreach (var particle in fd) {
                particle.ZBuffering();
            }

            t++;
        }
        return fd.Count;
    }

    // Which particle will stay closest to position <0,0,0> in the long term?
     public object PartOne(string fileData) {
        var particles = ParticleParse(fileData);
        return (
            from fd in particles
            orderby fd.acceleration.Len(), fd.velocity.Len(), fd.position.Len()
            select fd
        ).First().i;
    }

}


