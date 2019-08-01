require "Define"
GameInit = {};

local this = GameInit;

function GameInit.Init()

    print("game init");
    require 'UITest';
    CS.UnityEngine.Object.Instantiate(CS.UnityEngine.Resources.Load("UITestPanel"));
end

function Event()
    print("execute event")
end