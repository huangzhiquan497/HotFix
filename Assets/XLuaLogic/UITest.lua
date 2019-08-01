print("ui test")
UITest = {}
local this = UITest;

local gameObject;
local transform;

function UITest.awake(obj)
    gameObject = obj;
    transform = obj.transform;
end

function UITest.start()

    print("ui test start")
    print("injected object", Name)
    local name = transform:Find("Name"):GetComponent(typeof(CS.UnityEngine.UI.Text));
    local inputField = gameObject:GetComponentInChildren(typeof(CS.UnityEngine.UI.InputField));
    local modifyBtn = transform:Find("Button");

    name.text = "test start";
    if modifyBtn ~= nil
    then
        modifyBtn:GetComponent(typeof(CS.UnityEngine.UI.Button)).onClick:AddListener(
                function()
                    print("click")
                    name.text = inputField.text
                end
        );
    end
end

function UITest.update()

    print("ui test update")
    if CS.UnityEngine.Input.GetMouseButtonDown(1) then

        CS.UnityEngine.Object.Destroy(gameObject);
    end
end

function UITest.ondestroy()

    print("on destory")
end