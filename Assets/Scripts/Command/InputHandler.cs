using System;
using System.Collections;
using System.Collections.Generic;
using Command;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerShooting playerShooting;

    private Queue<Command.Command> _commands = new Queue<Command.Command>();

    private void FixedUpdate()
    {
        Command.Command moveCommand = InputMoveHandling();
        if (moveCommand != null)
        {
            _commands.Enqueue(moveCommand);
            moveCommand.Execute();
        }
    }

    private void Update()
    {
        Command.Command shootCommand = InputShootHandling();
        if (shootCommand != null)
        {
            shootCommand.Execute();
        }
    }

    private Command.Command InputMoveHandling()
    {
        if (Input.GetKey(KeyCode.D))
        {
            return new MoveCommand(playerMovement, 1, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            return new MoveCommand(playerMovement, -1, 0);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            return new MoveCommand(playerMovement, 0, 1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            return new MoveCommand(playerMovement, 0, -1);
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            //Undo movement
            return Undo();
        }
        else
        {
            return new MoveCommand(playerMovement, 0, 0); ;
        }
    }

    private Command.Command InputShootHandling()
    {
        return Input.GetButtonDown("Fire1") ? new ShootCommand(playerShooting) : null;
    }

    private Command.Command Undo()
    {
        if (_commands.Count > 0)
        {
            Command.Command undoCommand = _commands.Dequeue();
            undoCommand.UnExecute();
        }

        return null;
    }
}
