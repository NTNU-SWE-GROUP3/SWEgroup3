using UnityEngine;
using UnityEngine.Assertions;


namespace InputWrapper {
    public static class Input {
        static bool touchSupported => UnityEngine.Input.touchSupported;
        static Touch? fakeTouch => SimulateTouchWithMouse.Instance.FakeTouch;

        public static bool GetButton(string buttonName) {
            return UnityEngine.Input.GetButton(buttonName);
        }

        public static bool GetButtonDown(string buttonName) {
            return UnityEngine.Input.GetButtonDown(buttonName);
        }

        public static bool GetButtonUp(string buttonName) {
            return UnityEngine.Input.GetButtonUp(buttonName);
        }

        public static bool GetMouseButton(int button) {
            return UnityEngine.Input.GetMouseButton(button);
        }

        public static bool GetMouseButtonDown(int button) {
            return UnityEngine.Input.GetMouseButtonDown(button);
        }

        public static bool GetMouseButtonUp(int button) {
            return UnityEngine.Input.GetMouseButtonUp(button);
        }

        public static int touchCount {
            get {
                if (touchSupported) {
                    return UnityEngine.Input.touchCount;
                } else {
                    return fakeTouch.HasValue ? 1 : 0;
                }
            }
        }

        public static Touch GetTouch(int index) {
            if (touchSupported) {
                return UnityEngine.Input.GetTouch(index);
            } else {
                Assert.IsTrue(fakeTouch.HasValue && index == 0);
                return fakeTouch.Value;
            }
        }

        public static Touch[] touches {
            get {
                if (touchSupported) {
                    return UnityEngine.Input.touches;
                } else {
                    return fakeTouch.HasValue ? new[] {fakeTouch.Value} : new Touch[0];
                }
            }
        }
    }

    internal class SimulateTouchWithMouse {
        static SimulateTouchWithMouse instance;
        float lastUpdateTime;
        Vector3 prevMousePos;
        Touch? fakeTouch;


        public static SimulateTouchWithMouse Instance {
            get {
                if (instance == null) {
                    instance = new SimulateTouchWithMouse();
                }

                return instance;
            }
        }

        public Touch? FakeTouch {
            get {
                update();
                return fakeTouch;
            }
        }

        void update() {
            if (Time.time != lastUpdateTime) {
                lastUpdateTime = Time.time;
                
                var curMousePos = UnityEngine.Input.mousePosition;
                var delta = curMousePos - prevMousePos;
                prevMousePos = curMousePos;

                fakeTouch = createTouch(getPhase(delta), delta);
            }
        }

        static TouchPhase? getPhase(Vector3 delta) {
            if (UnityEngine.Input.GetMouseButtonDown(0)) {
                return TouchPhase.Began;
            } else if (UnityEngine.Input.GetMouseButton(0)) {
                return delta.sqrMagnitude < 0.01f ? TouchPhase.Stationary : TouchPhase.Moved;
            } else if (UnityEngine.Input.GetMouseButtonUp(0)) {
                return TouchPhase.Ended;
            } else {
                return null;
            }
        }

        static Touch? createTouch(TouchPhase? phase, Vector3 delta) {
            if (!phase.HasValue) {
                return null;
            }
            
            var curMousePos = UnityEngine.Input.mousePosition;
            return new Touch {
                phase = phase.Value,
                type = TouchType.Indirect,
                position = curMousePos,
                rawPosition = curMousePos,
                fingerId = 0,
                tapCount = 1,
                deltaTime = Time.deltaTime,
                deltaPosition = delta
            };
        }
    }
}