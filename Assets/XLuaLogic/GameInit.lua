require "Define"
GameInit = {};

local this = GameInit;

function GameInit.Init()

    print("game init");
    require 'UITest';
    CS.UnityEngine.Object.Instantiate(CS.UnityEngine.Resources.Load("UITestPanel"));
    require 'TestHotfix';

    TestHotfix.Add(4, 3);
    TestHotfix.DOHotfix();
    TestHotfix.Add(4, 3);
end

function Event()
    print("execute event")
end