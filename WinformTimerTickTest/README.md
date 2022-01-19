# 목적
- Winform Timer Tick 안에서 Winform Timer Enabled 속성값을 조작하여 Tick이 중복으로 발생하는 것을 막을 수 있다는 주장이 틀렸음을 보인다.

# 시나리오
1. 윈폼타이머의 인터벌을 짧게 준다음, 타이머 Tick에서 무거운 작업을 수행하여 중복으로 Tick이 발생하는지 Break Point 또는 Console Log로 확인한다.

# 결론
- Winform Timer는 UI Thread에서 동작하는, Worker Thread와는 무관한 것으로, 무거운 동작으로 인해 Tick이 중복으로 발생할 수 없다.
- Winform Timer Tick 안에서 **중복 Tick을 피하기 위한 목적**의 Enabled Property 조작은 의미없다.
- [정성태님의 Timer 설명](https://forum.dotnetdev.kr/t/winform-timer-tick-enabled/429/7)에 의하면 Interval은 Tick이 완전히 종료된 후 부터 Count를 시작한다고 한다.