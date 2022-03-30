﻿using OtusSpaceships.Interfaces;

namespace OtusSpaceships.Commands
{
    public class MoveCommand
    {
        private readonly IMovable _movable;

        public MoveCommand(IMovable movable)
        {
            _movable = movable;
        }

        public void Execute()
        {
            _movable.Position += _movable.Speed;
        }
    }
}
