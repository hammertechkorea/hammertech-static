@echo off
title 홈페이지 업데이트 도구
echo ==========================================
echo  홈페이지 업데이트를 시작합니다...
echo ==========================================
echo.
echo [1/4] 최신 파일 가져오는 중 (git pull)...
git pull
if %errorlevel% neq 0 (
    echo [에러] 최신 파일을 가져오지 못했습니다. 인터넷 연결이나 권한을 확인해주세요.
    pause
    exit /b
)
echo.
echo [2/4] 수정된 파일 추가하는 중 (git add)...
git add .
echo.
echo [3/4] 변경 사항 저장하는 중 (git commit)...
set /p commit_msg="수정 내용을 간단히 입력하세요 (예: 연락처 변경) [기본값: 수정함]: "
if "%commit_msg%"=="" set commit_msg=수정함
git commit -m "%commit_msg%"
echo.
echo [4/4] 홈페이지 올리는 중 (git push)...
git push
if %errorlevel% neq 0 (
    echo [에러] 홈페이지에 반영하지 못했습니다. gh auth login이 완료되었는지 확인해주세요.
    pause
    exit /b
)
echo.
echo ==========================================
echo  업데이트 완료! 약 1~2분 뒤 반영됩니다.
echo  확인 시 Ctrl + Shift + R로 강력 새로고침 하세요.
echo ==========================================
pause
