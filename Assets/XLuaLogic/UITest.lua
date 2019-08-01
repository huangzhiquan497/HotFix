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
    local name = Name:GetComponent(typeof(CS.UnityEngine.UI.Text));
    local inputField = gameObject:GetComponentInChildren(typeof(CS.UnityEngine.UI.InputField));
    local modifyBtn = transform:Find("Button"):GetComponent(typeof(CS.UnityEngine.UI.Button));

    name.text = "test start";
    print(typeof(modifyBtn))
    modifyBtn.onClick():AddListener(
            function()
                print("click")
                name = inputField.text
            end
    );

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