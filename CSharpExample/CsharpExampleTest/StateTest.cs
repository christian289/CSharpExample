using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace CsharpExampleTest
{
    // 상태패턴의 핵심
    // 1. 상태과 행동을 한 곳으로 모아서 실행.
    // 2. 상태와 행동이 사전에 정의되어있을 것.
    // 3. 당연한 말이지만 2번의 이유로, 상태에 대한 행동이 정적일 것. (불변)

    public enum EState
    {
        [State(EInternalEnum.A1)]
        A,
        [State(EInternalEnum.B1)]
        B,
        [State(EInternalEnum.C1)]
        C,
        [State(EInternalEnum.D1)]
        D,
    }

    #region 조건문을 활용한 상태패턴 (가장 덜 객체지향적이고 별로인 듯...캡슐화도 안되고...)
    public class IfConditionStatePatternExam
    {
        // EnumAnonymousMethodStatePatternExam 과 유사하게 
        // 특정 상태에 따른 동작을 DoStateAction에 정의해놓고 조건문으로 분기하여 행동한다.
        // 상태라는 것은 DDD의 값 객체(ValueObject)이므로 모델마다 불변하게 작성해야한다.
        // 따라서 행동 메서드에 인자를 두지 않는다. (이게 중요)
        // 상태가 변화한다면 객체를 새로 생성하는 코딩컨디션을 가지고 가야한다.

        private readonly EState state;

        public IfConditionStatePatternExam(EState state)
        {
            this.state = state;
        }

        public static string DoAllStateAction()
        {
            // 다 실행시켜야하지만 상태별로 행동이 메서드로 캡슐화 되어있지 않고 조건문안에 로직으로 퍼져있어 실행 불가능.
            // 굳이 실행시키고 싶으면 로직 안에있는거 복붙하던가 ㅋㅎㅋㅎ

            return string.Empty;
        }

        public string DoStateAction()
        {
            if (state == EState.A)
            {
                return "카드 A를 세팅!";
            }
            else if (state == EState.B)
            {
                return "엎어놓은 카드 B를 발동!";
            }
            else if (state == EState.C)
            {
                return "에노미 컨트롤러 핫츠도!";
            }
            else if (state == EState.D)
            {
                return "히다리 미기 A B 하하하하 와타시노 쇼리다";
            }
            else
            {
                return default;
            }
        }
    }
    #endregion

    #region Dictionary와 익명메서드를 활용한 상태 패턴
    public class EnumAnonymousMethodStatePatternExam
    {
        // 상태와 행동을 한 곳에 등록(세팅)해둔다.
        // 추후 상태와 행동이 추가될 때 이 클래스에만 추가해주면 되며,
        // DoState 메서드에 인자로 받는 상태Enum에 따라 정해진 행동을 수행한다.
        private static readonly Dictionary<EState, Func<string, string>> _StateAction;
        private readonly EState state;

        public EnumAnonymousMethodStatePatternExam(EState state)
        {
            this.state = state;
        }

        static EnumAnonymousMethodStatePatternExam()
        {
            _StateAction = new Dictionary<EState, Func<string, string>>
            {
                { EState.A, AStateMethod },
                { EState.B, BStateMethod },
                { EState.C, CStateMethod },
                { EState.D, DStateMethod }
            };
        }

        private static string AStateMethod(string pstr)
        {
            return $"{pstr}\r\nAStateMethod 실행";
        }

        private static string BStateMethod(string pstr)
        {
            return $"{pstr}\r\nBStateMethod 실행";
        }

        private static string CStateMethod(string pstr)
        {
            return $"{pstr}\r\nCStateMethod 실행";
        }

        private static string DStateMethod(string pstr)
        {
            return $"{pstr}\r\nDStateMethod 실행";
        }

        public static string DoAllStateAction()
        {
            StringBuilder sb = new();

            foreach (KeyValuePair<EState, Func<string, string>> item in _StateAction)
            {
                sb.AppendLine(item.Value("하하하하하하하하하하하하하하하하하하하하"));
            }

            return sb.ToString();
        }

        public string DoStateAction()
        {
            return _StateAction[state].Invoke("특정 상태값 발동!");
        }
    }
    #endregion

    #region 속성과 조건문을 이용한 상태 패턴
    public class AttributeStatePatternExam
    {
        private readonly EState state;

        public AttributeStatePatternExam(EState state)
        {
            this.state = state;
        }

        public static string DoAllStateAction()
        {
            StringBuilder sb = new();
            sb.AppendLine(EState.A.EnumExeMethod());
            sb.AppendLine(EState.B.EnumExeMethod());
            sb.AppendLine(EState.C.EnumExeMethod());
            sb.AppendLine(EState.D.EnumExeMethod());

            return sb.ToString();
        }

        public string DoStateAction()
        {
            return state.EnumExeMethod();
        }
    }

    public enum EInternalEnum
    {
        A1,
        B1,
        C1,
        D1,
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class StateAttribute : Attribute
    {
        public EInternalEnum State { get; init; }

        public StateAttribute(EInternalEnum state)
        {
            State = state;
        }

        public string DoStateAction()
        {
            // 유지보수 하면서 분기가 추가된다면 여기서만 추가할 경우 조건문이 분산되지 않는다.
            if (State == EInternalEnum.A1)
            {
                return "카드 A를 세팅!";
            }
            else if (State == EInternalEnum.B1)
            {
                return "엎어놓은 카드 B를 발동!";
            }
            else if (State == EInternalEnum.C1)
            {
                return "에노미 컨트롤러 핫츠도!";
            }
            else if (State == EInternalEnum.D1)
            {
                return "히다리 미기 A B 하하하하 와타시노 쇼리다";
            }
            else
            {
                return default;
            }
        }
    }

    public static class StringValueStaticMethod
    {
        public static string EnumExeMethod<T>(this T value) where T : Enum
        {
            Type type = value.GetType();
            FieldInfo fi = type.GetField(value.ToString());
            StateAttribute[] attrs = fi.GetCustomAttributes(typeof(StateAttribute), false) as StateAttribute[];

            if (attrs.Length > 0)
            {
                return attrs[0].DoStateAction();
            }

            return default;
        }
    }
    #endregion

    #region 추상클래스를 이용한 '정적' 상태 클래스
    public class AbstractClassStatePatternExam
    {
        private readonly State state;

        public AbstractClassStatePatternExam(State state)
        {
            this.state = state;
        }

        public static string DoAllStateAction()
        {
            List<State> states = new()
            {
                new AState(),
                new BState(),
                new CState(),
                new DState(),
            };
            StringBuilder sb = new();
            
            foreach (State state in states)
            {
                sb.AppendLine(state.Action());
            }

            return sb.ToString();
        }

        public string DoStateAction()
        {
            return state.Action();
        }
    }

    public abstract class State
    {
        public EState MyState { get; init; }

        public State(EState state)
        {
            MyState = state;
        }

        public abstract string Action();
    }

    public class AState : State
    {
        public AState() : base(EState.A)
        {
        }

        public override string Action()
        {
            return $"나는 {MyState} 입니다.";
        }
    }

    public class BState : State
    {
        public BState() : base(EState.B)
        {
        }

        public override string Action()
        {
            return $"나는 {MyState} 인데요?";
        }
    }

    public class CState : State
    {
        public CState() : base(EState.C)
        {
        }

        public override string Action()
        {
            return $"나는 {MyState} 임 ㅋ";
        }
    }

    public class DState : State
    {
        public DState() : base(EState.D)
        {
        }

        public override string Action()
        {
            return $"나 {MyState} 라고";
        }
    }
    #endregion

    #region 인터페이스를 이용한 '정적' 상태 클래스
    public class InterfaceStatePatternExam
    {
        private readonly IStateAction state;

        public InterfaceStatePatternExam(IStateAction state)
        {
            this.state = state;
        }

        public static string DoAllStateAction()
        {
            List<IStateAction> states = new()
            {
                new AStateObject(),
                new BStateObject(),
                new CStateObject(),
                new DStateObject(),
            };
            StringBuilder sb = new();
            
            foreach (IStateAction state in states)
            {
                sb.AppendLine(state.DoStateAction());
            }

            return sb.ToString();
        }

        public string DoStateAction()
        {
            return state.DoStateAction();
        }
    }

    public interface IStateAction
    {
        EState State { get; init; }

        string DoStateAction();
    }

    public class AStateObject : IStateAction
    {
        public EState State { get; init; }

        public AStateObject()
        {
            State = EState.A;
        }

        public string DoStateAction()
        {
            return "나는 현재 A 상태입니다.";
        }
    }

    public class BStateObject : IStateAction
    {
        public EState State { get; init; }

        public BStateObject()
        {
            State = EState.B;
        }

        public string DoStateAction()
        {
            return "나는 현재 B 상태입니다.";
        }
    }

    public class CStateObject : IStateAction
    {
        public EState State { get; init; }

        public CStateObject()
        {
            State = EState.C;
        }

        public string DoStateAction()
        {
            return "나는 현재 C 상태입니다.";
        }
    }

    public class DStateObject : IStateAction
    {
        public EState State { get; init; }

        public DStateObject()
        {
            State = EState.D;
        }

        public string DoStateAction()
        {
            return "나는 현재 D 상태입니다.";
        }
    }
    #endregion

    public class StateTest
    {
        private readonly ITestOutputHelper output;

        public StateTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        void 추상클래스상태패턴실행()
        {
            output.WriteLine(AbstractClassStatePatternExam.DoAllStateAction());

            AbstractClassStatePatternExam check = new(new AState());
            output.WriteLine(check.DoStateAction());
        }

        void 인터페이스상태패턴실행()
        {
            output.WriteLine(InterfaceStatePatternExam.DoAllStateAction());

            InterfaceStatePatternExam check = new(new BStateObject());
            output.WriteLine(check.DoStateAction());
        }

        void Enum익명메서드상태패턴실행()
        {
            output.WriteLine(EnumAnonymousMethodStatePatternExam.DoAllStateAction());

            EnumAnonymousMethodStatePatternExam check = new(EState.C);
            output.WriteLine(check.DoStateAction());
        }

        void If문상태패턴실행()
        {
            output.WriteLine(IfConditionStatePatternExam.DoAllStateAction());

            IfConditionStatePatternExam check = new(EState.A);
            output.WriteLine(check.DoStateAction());
        }

        void EnumAttribute상태패턴실행()
        {
            output.WriteLine(AttributeStatePatternExam.DoAllStateAction());

            AttributeStatePatternExam check = new(EState.D);
            output.WriteLine(check.DoStateAction());
        }

        [Fact]
        public void Test()
        {
            추상클래스상태패턴실행();
            output.WriteLine("\r\n");
            output.WriteLine("\r\n");
            인터페이스상태패턴실행();
            output.WriteLine("\r\n");
            output.WriteLine("\r\n");
            Enum익명메서드상태패턴실행();
            output.WriteLine("\r\n");
            output.WriteLine("\r\n");
            If문상태패턴실행();
            output.WriteLine("\r\n");
            output.WriteLine("\r\n");
            EnumAttribute상태패턴실행();
        }
    }
}
