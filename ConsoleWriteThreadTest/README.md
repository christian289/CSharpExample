# ConsoleWriteThreadTest

- ConsoleApp과 WinformApp의 Thread Id 변화를 디버깅으로 확인합니다.
- 동기화 컨텍스트의 유무로, ConsoleApp은 Task이후 Thread Id가 유지되는 반면, WinformApp은 Task 이후 Thread Id가 원래대로 돌아옵니다.