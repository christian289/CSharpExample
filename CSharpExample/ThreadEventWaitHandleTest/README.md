# ThreadEventWaitHandleTest

.NET 레거시 Thread의 동기화 방식 중 하나인 WaitHandle을 이용하는 방법입니다.
간단하게는 신호가 오기 전까지 차단되다가 신호가 오면 동작하라는 뜻입니다.
EventWaitHandle 클래스에 생성자로 아래와 같이 지정합니다.

```cs
new EventWaitHandle(false, EventResetMode.ManualReset);
```

main Thread가 돌고있고 Thread t를 통해 worker thread가 threadFunc 라는 기본함수를 뼈대로 동작하기 시작합니다.
Main Thread 에서는 EventWaitHandle.WaitOne()을 통해 Worder Thread에서 신호가 오는 것을 대기하면서 Main Thread는 차단하게 됩니다.
예제와 같이 Worker Thread 쪽에서 parameter로 인계받은 객체에 EventWaitHandle.Set() 메서드를 호출하면 Main Thread 쪽으로 신호가 가고, WaitOne을 통과하게 됩니다.

이런 식으로 각각의 Thread를 멈추면서 서로 데이터를 동기화 하는데 이용할 수 있습니다.

threadFunc 의 내용을 부분 부분 주석처리하면서 테스트해보시면 금방 감을 잡을 수 있습니다.