# 목적
- Winform의 UI Thread는 Thread Pool에 속한 Thread인지 체크한다.
- Thread Pool의 목적은 짧은 작업(Task)을 Thread를 생성할 때 발생하는 overhead를 줄이기 위해 Thread를 미리 일정량 생성시켜놔서 사용할 때는 overhead 없이 사용하기 위함이다.
- 따라서 개념상 프로세스의 시작부터 끝까지 Thread가 유지되어야 하는 UI Thread는 Thread Pool과 관련이 없어야 한다.

# 결론
- Winform의 UI Thread는 Thread Pool의 Thread가 아니다.