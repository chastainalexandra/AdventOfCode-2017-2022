namespace AdventOfCode.Y2017.Day24;


// Each component has two ports, one on each end. 
// The ports come in all different types, and only matching types can be connected.
//  You take an inventory of the components by their port types (your puzzle input). 
// Each port is identified by the number of pins it uses; more pins mean a stronger connection for your bridge.
public class Component {
    public int Port1;
    public int Port2;
}