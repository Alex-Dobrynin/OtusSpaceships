// See https://aka.ms/new-console-template for more information
using VectorAndPoint.ValTypes;

Console.WriteLine("Hello, World!");



var Velocity = new Vector(5, 5);

var x = 0 + Velocity.Length * Math.Cos(45.0 / 360.0 * (2.0 * Math.PI));

Console.WriteLine(x);
