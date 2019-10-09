with(__oznamespace__){
__oznamespace__.OZCStringResource = function() {
	__import__("OZCStringResource");
	var ___arguments___ = arguments; if(__isCasting__(___arguments___, this)) return ___arguments___[0];
	this.__constructor__OZCStringResource();
};
OZCStringResource.__supername__ = "null";
OZCStringResource.__define_static_class__ = function() {
	__define_static__(OZCStringResource);
	OZCStringResource.GetStringResource = function(id) {
		if(CLocale.GetBaseLang_Flash().localeCompare("ko-KR") == 0) {
			switch (id) {
				case OZCStringResource.IDS_OPT_SAVEAS_DEFAULT:
					return "기본 옵션";
				case OZCStringResource.IDS_OPT_SAVEAS:
					return "저장 옵션";
				case OZCStringResource.IDS_OPT_SAVE_WAY_TXT:
					return "보고서를 한 페이지/페이지별 로 바인딩한 후 저장합니다.";
				case OZCStringResource.IDS_OPT_SAVE_WAY_ONEPAGE:
					return "한페이지로 저장";
				case OZCStringResource.IDS_OPT_SAVE_WAY_EACHPAGE:
					return "페이지별로 저장";					
				case OZCStringResource.IDS_OPT_SAVE_WAY:
					return "저장 방식";
				case OZCStringResource.IDS_OPT_SAVE_ACTION:
					return "실행";
				case OZCStringResource.IDS_OPT_SAVE_MULTIDOC:
					return "다중 문서";
				case OZCStringResource.IDS_OPT_SAVE_AREA:
					return "저장 영역";
				case OZCStringResource.IDS_OPT_SAVE_COMPONENT:
					return "컴포넌트";
				case OZCStringResource.IDS_OPT_SAVE_LINK_INFO:
					return "링크정보 저장";
				case OZCStringResource.IDS_OPT_ACTION_AFTER_SAVE:
					return "저장 후 바로 실행";					
				case OZCStringResource.IDS_OPT_SAVE_ONEFILE:
					return "한파일로 저장";					
				case OZCStringResource.IDS_OPT_SAVE_ALLFILE:
					return "모든 문서 저장";					
				case OZCStringResource.IDS_OPT_ALLPAGE:
					return "전체 페이지";					
				case OZCStringResource.IDS_OPT_CURRENTPAGE:
					return "현재 페이지";					
				case OZCStringResource.IDS_OPT_SELECTEDPAGE:
					return "선택된 페이지";		
				case OZCStringResource.IDS_OPT_CHOOSEPAGE:
					return "페이지 지정 : ";
				case OZCStringResource.IDS_OPT_CHOOSEPAGE_DETAIL:
					return "페이지 번호 및/또는 페이지 범위를 쉼표(,)로 구분하여 입력하십시오.";												
				case OZCStringResource.IDS_OPT_CHOOSEPAGE_DETAIL2:
					return "예) 1,3,5-12";												
				case OZCStringResource.IDS_OPT_COMPONENT_LABEL://IDS_MSG_COMP_NAME_LABEL
					return "저장할 컴포넌트를 설정합니다.";					
				case OZCStringResource.IDS_OPT_LABEL://IDS_MSG_COMP_NAME_LABEL
					return "라벨";
				case OZCStringResource.IDS_OPT_IMAGE://IDS_MSG_COMP_NAME_BARCODE
					return "이미지";
				case OZCStringResource.IDS_OPT_BARCODE:
					return "바코드";					
				case OZCStringResource.IDS_OPT_PDF417:
					return "PDF417 바코드";					
				case OZCStringResource.IDS_OPT_QR_BARCODE:
					return "QR 바코드";					
				case OZCStringResource.IDS_OPT_HTML:
					return "HTML";	
				case OZCStringResource.IDS_OPT_CHART:
					return "차트";			
				case OZCStringResource.IDS_OPT_LINE:
					return "라인";			
				case OZCStringResource.IDS_OPT_CIRCLE:
					return "원";			
				case OZCStringResource.IDS_OPT_RECTANGLE:
					return "사각형";			
				case OZCStringResource.IDS_OPT_ARROW:
					return "화살표";								
				case OZCStringResource.IDS_OPT_URL_LINK:
					return "URL 링크";			
				case OZCStringResource.IDS_OPT_LIST_LINK:
					return "목차 링크";		
				case OZCStringResource.IDS_OPT_PDF_SAVE:
					return "PDF 저장 옵션";		
				case OZCStringResource.IDS_OPT_DOC_IMFOR:
					return "문서 정보";			
				case OZCStringResource.IDS_OPT_OPEN_PROTECT:
					return "열기 보호";					
				case OZCStringResource.IDS_OPT_EDIT_PROTECT:
					return "편집 보호";
				case OZCStringResource.IDS_OPT_USER_RIGHT:
					return "사용자 접근 권한";							
				case OZCStringResource.IDS_OPT_TITLE:
					return "제목";
				case OZCStringResource.IDS_OPT_WRITER:
					return "작성자";			
				case OZCStringResource.IDS_OPT_SUBJECT:
					return "주제";			
				case OZCStringResource.IDS_OPT_KEYWORD:
					return "키워드";
				case OZCStringResource.IDS_OPT_APPLICATION:
					return "응용프로그램";			
				case OZCStringResource.IDS_OPT_USER_PW:
					return "사용자 암호";
				case OZCStringResource.IDS_OPT_USER_PW_CONFIRM:
					return "사용자 암호 확인";						
				case OZCStringResource.IDS_OPT_MASTER_PW:
					return "마스터 암호";
				case OZCStringResource.IDS_OPT_MASTER_PW_CONFIRM:
					return "마스터 암호 확인";		
				case OZCStringResource.IDS_OPT_PRINT_PERMIT:
					return "인쇄 허용";
				case OZCStringResource.IDS_OPT_CONTENTS_COPY:
					return "내용 복사";
				case OZCStringResource.IDS_OPT_SAVEIMG_TOCHART:
					return "차트를 이미지로 저장";
				case OZCStringResource.IDS_OPT_FONT_INCLUDE:
					return "폰트 포함";			
				case OZCStringResource.IDS_OPT_PRINT:
					return "인쇄";
				case OZCStringResource.IDS_OPT_PRINT_COUNT:
					return "인쇄 매수";		
				case OZCStringResource.IDS_OPT_PRINT_SCOPE:
					return "인쇄 범위";
				case OZCStringResource.IDS_OPT_PRINT_RULE:
					return "인쇄 방식";
				case OZCStringResource.IDS_OPT_COUNT:
					return "매";
				case OZCStringResource.IDS_OPT_PRINT_ONEPAGE:
					return "한 부씩 인쇄";
				case OZCStringResource.IDS_OPT_PRINT_ALL_REPORT:
					return "모든 리포트 인쇄";					
				case OZCStringResource.IDS_OPT_ALL:
					return "모두";					
				case OZCStringResource.IDS_OPT_CHOOSE_PAGE:
					return "페이지 지정:";	
				case OZCStringResource.IDS_OPT_PRINT_FITTOPAGE:
					return "용지에 맞춰 인쇄";					
				case OZCStringResource.IDS_OPT_DEVIDE_PRINT:
					return "분할인쇄";
				case OZCStringResource.IDS_OPT_DEVIDE_PRINT_TOFITPAPER:
					return "용지에 맞춰서 분할인쇄";					
				case OZCStringResource.IDS_TOGETHER_PRINT:
					return "모아찍기";					
				case OZCStringResource.IDS_OPT_PAGECOUNT_TOPRINT:
					return "한 페이지에 인쇄할 페이지 수";	
				case OZCStringResource.IDS_OPT_PRINT_ORDER:
					return "인쇄 순서";										
				case OZCStringResource.IDS_OPT_HORIZON:
					return "수평";										
				case OZCStringResource.IDS_OPT_VERTICAL:
					return "수직";										
				case OZCStringResource.IDS_OPT_PRINT_DIRECTION:
					return "인쇄 방향";										
				case OZCStringResource.IDS_OPT_PORTRAIT:
					return "세로";										
				case OZCStringResource.IDS_OPT_LANDSCAPE:
					return "가로";										
				case OZCStringResource.IDS_OPT_OPTION:
					return "옵션";		
				case OZCStringResource.IDS_OPT_CONFIRM:
					return "확인";		
				case OZCStringResource.IDS_OPT_CANCEL:
					return "취소";		
				case OZCStringResource.IDS_OPT_SAVE:
					return "저장";	
				case OZCStringResource.IDS_OPT_OZD_SAVE_OPTION:
					return "저장 옵션";								
				case OZCStringResource.IDS_OPT_PASSWORD:
					return "암호";		
				case OZCStringResource.IDS_OPT_MEMOADD_OK:
					return "편집 내용 포함";		
				case OZCStringResource.IDS_OPT_SAVE_ALLREPORT:
					return "모든 보고서 저장";						
				case OZCStringResource.IDS_OPT_INCLUDE_IMG:
					return "이미지 포함";
				case OZCStringResource.IDS_OPT_FILE_TYPE:
					return "파일 형식";					
				case OZCStringResource.IDS_OPT_EXCEL_SAVE:
					return "Excel 저장 옵션";
				case OZCStringResource.IDS_OPT_EXCEL_SAVE2:
					return "Excel 저장 옵션(고급)";
				case OZCStringResource.IDS_OPT_NEXCEL_SAVE:
					return "Nexcel 저장 옵션";
				case OZCStringResource.IDS_OPT_WORD_SAVE:
					return "Word 저장 옵션";						
				case OZCStringResource.IDS_OPT_PPT_SAVE:
					return "PowerPoint 저장 옵션";		
				case OZCStringResource.IDS_OPT_HTML_SAVE:
					return "HTML 저장 옵션";		
				case OZCStringResource.IDS_OPT_CSV_SAVE:
					return "CSV 저장 옵션";		
				case OZCStringResource.IDS_OPT_TEXT_SAVE:
					return "TEXT 저장 옵션";		
				case OZCStringResource.IDS_OPT_JPG_SAVE:
					return "JPG 저장 옵션";
				case OZCStringResource.IDS_OPT_TIFF_SAVE:
					return "TIFF 저장 옵션";		
				case OZCStringResource.IDS_OPT_HWP_SAVE:
					return "한글 저장 옵션";		
				case OZCStringResource.IDS_OPT_HWP97_SAVE:
					return "한글97 저장 옵션";		
				case OZCStringResource.IDS_OPT_MHT_SAVE:
					return "MHT 저장 옵션";			
				case OZCStringResource.IDS_OPT_SAVE_FORM:
					return "저장 형태";							
				case OZCStringResource.IDS_OPT_PAGE:
					return "페이지";
				case OZCStringResource.IDS_OPT_FONT:
					return "글꼴";
				case OZCStringResource.IDS_OPT_FORMAT:
					return "서식";
				case OZCStringResource.IDS_OPT_AUTO_FIT:
					return "자동 맞춤";
				case OZCStringResource.IDS_OPT_CELL_MINIMUM_SIZE_CELL:
					return "셀 최소 크기";
				case OZCStringResource.IDS_OPT_SAVE_MODE:
					return "저장 형식";			
				case OZCStringResource.IDS_OPT_KEEP_FORM_ATTR:
					return "폼 형태, 속성 유지";			
				case OZCStringResource.IDS_OPT_IGNORE_COLOR_OUTLINE_CELL:
					return "색상, 외곽라인, 셀합치기 무시";
				case OZCStringResource.IDS_OPT_REMOVE_CELL_AFTERDIVIDE:
					return "분할 후 주변 셀 제거";
				case OZCStringResource.IDS_OPT_SAVE_SHEET_PER_COLPAGE:
					return "페이지 열별 시트 저장";
				case OZCStringResource.IDS_OPT_SAVE_SHEET_PAGE:
					return "페이지별 시트 저장";
				case OZCStringResource.IDS_OPT_COLUMN_FIRST:
					return "Column 우선";
				case OZCStringResource.IDS_OPT_ROW_FIRST:
					return "Row 우선";
				case OZCStringResource.IDS_OPT_REMOVE_PAGE_GAP:
					return "페이지 사이 공백 제거";
				case OZCStringResource.IDS_OPT_SIZE_COLON:
					return "크기 :";
				case OZCStringResource.IDS_OPT_REMOVE_LINE:
					return "라인 제거";		
				case OZCStringResource.IDS_OPT_EXCLUDE_FIRST_PAGE:
					return "첫 페이지 제외";
				case OZCStringResource.IDS_OPT_ONLY_FIRST_PAGE:
					return "첫 페이지만";
				case OZCStringResource.IDS_OPT_ALL_PAGE:
					return "모든 페이지";						
				case OZCStringResource.IDS_OPT_LINE_SCOPE_LINE_NUMBER:
					return "라인 범위 및/또는 라인 번호를 콤마(,)로 구분하여 입력하십시오 \n 예)1-3,5,12";					
				case OZCStringResource.IDS_OPT_CHANGE_TXT_TO_INTEGER:
					return "숫자형 텍스트를 숫자로 변환";
				case OZCStringResource.IDS_OPT_APPLY_ONLYLABEL_NUMBER:
					return "표시 형식이 숫자인 라벨만 적용";
				case OZCStringResource.IDS_OPT_CHANGE_AS_NORMAL:
					return "일반(셀표시 형식)으로 변환";	
				case OZCStringResource.IDS_OPT_FONT_COLON:
					return "글꼴:";
				case OZCStringResource.IDS_OPT_WIDTH:
					return "너비";
				case OZCStringResource.IDS_OPT_HEIGHT:
					return "높이";
				case OZCStringResource.IDS_OPT_SAVE_MHT_TYPE:
					return "mht 형식으로 저장 (오피스 XP 이상 지원)";					
				case OZCStringResource.IDS_OPT_TABLE_SAVE_WAY:
					return "테이블 저장 방식";
				case OZCStringResource.IDS_OPT_TABLE_TYPE_WAY:
					return "테이블 형식으로 저장";
				case OZCStringResource.IDS_OPT_BAND_SAVE_WAY:
					return "밴드 저장 방식";
				case OZCStringResource.IDS_OPT_BAND_SAVE_PAGE:
					return "페이지 헤더/풋터 밴드를 머리말/꼬리말에 저장";		
				case OZCStringResource.IDS_OPT_PPT_SAVE_WAY:
					return "PowerPoint 저장 방식";
				case OZCStringResource.IDS_OPT_MHTD_SAVE_PAGE:
					return "mht 형식으로 저장(오피스 XP 이상 지원)";		
				case OZCStringResource.IDS_OPT_FONT_SIZE_SETTING:
					return "글꼴 크기 설정";
				case OZCStringResource.IDS_OPT_PAGE_BETWEEN:
					return "페이지 간격";
				case OZCStringResource.IDS_OPT_PER_PAGE_SAVE:
					return "페이지별 저장";								
				case OZCStringResource.IDS_OPT_LINE_COUNT_COLON:
					return "라인수:";	
				case OZCStringResource.IDS_OPT_OFFSET_COLON:
					return "오프셋:";
				case OZCStringResource.IDS_OPT_STARTPOINT_HTMLDOC:
					return "HTML 문서의 시작좌표를 지정합니다.";					
				case OZCStringResource.IDS_OPT_ENCODING_COLON:
					return "인코딩 :";					
				case OZCStringResource.IDS_OPT_X_AXIS:
					return "X축";							
				case OZCStringResource.IDS_OPT_Y_AXIS:
					return "Y축";	
				case OZCStringResource.IDS_OPT_OFFSET:
					return "오프셋";
				case OZCStringResource.IDS_OPT_SEPARATION:
					return "구분";							
				case OZCStringResource.IDS_OPT_SEPARATOR:
					return "구분자";	
				case OZCStringResource.IDS_OPT_USER_DEFINE:
					return "사용자 정의";						
				case OZCStringResource.IDS_OPT_SEPARATION_TYPE:
					return "구분형태";
				case OZCStringResource.IDS_OPT_PAGE_BRACKET:
					return "<PAGE>";
				case OZCStringResource.IDS_OPT_SET_PAGELINE:
					return "페이지간 간격 줄 수를 설정합니다.";
				case OZCStringResource.IDS_OPT_CHAR_NUMBER:
					return "숫자문자";					
				case OZCStringResource.IDS_OPT_ADD_SEPARATOR:
					return "자릿수 구분기호 추가";					
				case OZCStringResource.IDS_OPT_REMOVE_SEPARATOR:
					return "자릿수 구분기호 제거";					
				case OZCStringResource.IDS_OPT_PAGE_SEPARATION:
					return "페이지 구분";		
				case OZCStringResource.IDS_OPT_LABEL_SAVE:
					return "라벨 저장 방식";		
				case OZCStringResource.IDS_OPT_LABEL_SIZE:
					return "라벨 크기 고정";
				case OZCStringResource.IDS_OPT_POSITION_SAVE:
					return "위치 저장 방식";
				case OZCStringResource.IDS_OPT_VERTREL_TOPARA:
					return "세로를 문단으로 설정";
				case OZCStringResource.IDS_OPT_BACKGROUND_BAND:
					return "백그라운드 밴드를 바탕쪽에 저장";	
				case OZCStringResource.IDS_OPT_TITLE_COLON:
					return "제목 :";	
				case OZCStringResource.IDS_OPT_DISPLAY_OPTION:
					return "창 옵션";	
				case OZCStringResource.IDS_OPT_GRIDLINES:
					return "눈금선";	
				case OZCStringResource.IDS_OPT_ENCODING_WAY:
					return "인코딩 방식";
				case OZCStringResource.IDS_OPT_ENCODING_G3:
					return "G3";	
				case OZCStringResource.IDS_OPT_ENCODING_G4:
					return "G4";		
				case OZCStringResource.IDS_OPT_ENCODING_SAVEAS_ONEFILE:
					return "한 파일에 저장";
				case OZCStringResource.IDS_OPT_RATIO:
					return "비율";	
				case OZCStringResource.IDS_OPT_ZOOM_INOUT:
					return "확대/축소 비율";
				case OZCStringResource.IDS_OPT_SET_DPI:
					return "해상도에 따라 크기 결정";
				case OZCStringResource.IDS_OPT_DPI:
					return "해상도";
				case OZCStringResource.IDS_OPT_SIZE:
					return "크기";					
				case OZCStringResource.IDS_OPT_SAVE_RATIO:
					return "저장 배율";
				case OZCStringResource.IDS_OPT_COMPRESSION:
					return "압축";
				case OZCStringResource.IDS_OPT_QUALITY:
					return "품질";			
				case OZCStringResource.IDS_OPT_USE_INDENT:
					return "들여쓰기 기능 사용";
				case OZCStringResource.IDS_OPT_DATA_SAVE:
					return "데이터 저장";		
				case OZCStringResource.IDS_OPT_FIND_WHAT:
					return "찾을 내용:";	
				case OZCStringResource.IDS_OPT_MATCH_WHOLE_WORD:
					return "단어 단위로";
				case OZCStringResource.IDS_OPT_MATCH_CASE:
					return "대 소문자 구분";
				case OZCStringResource.IDS_OPT_DIRECTION:
					return "방향";			
				case OZCStringResource.IDS_OPT_SEARCH_UP:
					return "위로";
				case OZCStringResource.IDS_OPT_SEARCH_DOWN:
					return "아래로";
				case OZCStringResource.IDS_OPT_NEXT:
					return "다음 찾기";			
				case OZCStringResource.IDS_OPT_REFRESH_REPORT:
					return "보고서 새로 고침";		
				case OZCStringResource.IDS_OPT_INTERVAL:
					return "주기";					
				case OZCStringResource.IDS_OPT_HH:
					return "시";		
				case OZCStringResource.IDS_OPT_MM:
					return "분";		
				case OZCStringResource.IDS_OPT_SS:
					return "초";							
				case OZCStringResource.IDS_OPT_REFRESH_NOW:
					return "새로 고침";		
				case OZCStringResource.IDS_OPT_START:
					return "시작";		
				case OZCStringResource.IDS_OPT_STOP:
					return "정지";				
				case OZCStringResource.IDS_OPT_LEFT_COLON:
					return "왼쪽:";
				case OZCStringResource.IDS_OPT_RIGHT_COLON:
					return "오른쪽:";
				case OZCStringResource.IDS_OPT_TOP_COLON:
					return "위쪽:";
				case OZCStringResource.IDS_OPT_BOTTOM_COLON:
					return "아래쪽:";		
				case OZCStringResource.IDS_OPT_PAGE_SETUP:
					return "페이지 설정";	
				case OZCStringResource.IDS_OPT_MARGIN:
					return "여백";	
				case OZCStringResource.IDS_OPT_UNIT:
					return "사용 단위계";		
				case OZCStringResource.IDS_OPT_UNIT_COLON:
					return "단위:";	
				case OZCStringResource.IDS_OPT_MARGIN_OPTION:
					return "여백 옵션";		
				case OZCStringResource.IDS_OPT_MARGIN_DEFAULT:
					return "기본 여백 사용";	
				case OZCStringResource.IDS_OPT_MARGIN_AUTO_ADJUST:
					return "자동 조정";		
				case OZCStringResource.IDS_OPT_HTML_VERSION:
					return "버전";		
				case OZCStringResource.IDS_OPT_HTML_VERSION_COLON:
					return "HTML 버전 :";	
				case OZCStringResource.IDS_OPT_TOTAL:
					return "전체";	
				case OZCStringResource.IDS_OPT_REPORT_STRUCTURE:
					return "보고서 구조보기";		
				case OZCStringResource.IDS_OPT_LABEL_FORMAT:
					return "라벨 서식";			
				case OZCStringResource.IDS_OPT_PARAGRAPH:
					return "단락";	
				case OZCStringResource.IDS_OPT_FILL:
					return "채우기";		
				case OZCStringResource.IDS_OPT_LINECOLOR:
					return "테두리";	
				case OZCStringResource.IDS_OPT_ETC:
					return "기타";		
				case OZCStringResource.IDS_OPT_ALIGNMENT:
					return "정렬";		
				case OZCStringResource.IDS_OPT_TXT_DIRECTION:
					return "텍스트방향";
				case OZCStringResource.IDS_OPT_INTERNAL_MARGIN:
					return "여백";		
				case OZCStringResource.IDS_OPT_PREVIEW:
					return "미리보기";	
				case OZCStringResource.IDS_OPT_HORIZON_COLON:
					return "수평 :";										
				case OZCStringResource.IDS_OPT_VERTICAL_COLON:
					return "수직 :";	
				case OZCStringResource.IDS_OPT_LINE_SPACE_COLON:
					return "줄 간격 :";										
				case OZCStringResource.IDS_OPT_INDENT_COLON:
					return "들여쓰기 :";
				case OZCStringResource.ID_OPT_PARAMETERS_TITLE_LABEL:
					return "패러미터 값을 수정합니다.";
				case OZCStringResource.IDS_OPT_EXIT:
					return "종료";
				case OZCStringResource.IDS_OPT_OZZPASSWORD:
					return "패스워드";		
				case OZCStringResource.IDS_OPT_OZZPASSWORD_CHECK:
					return "패스워드 확인";			
				case OZCStringResource.IDS_OPT_ID:
					return "아이디";		
				case OZCStringResource.IDS_OPT_SECURITY:
					return "보안";							
				case OZCStringResource.IDS_OPT_PRODUCT_INFO:
					return "제품정보";		
				case OZCStringResource.IDS_OPT_OZ_INFO:
					return "제품 정보";		
				case OZCStringResource.IDS_OPT_ERROR_CODE_COLON:
					return "에러코드:";			
				case OZCStringResource.IDS_OPT_GENERAL_MSG_COLON:
					return "일반메시지:";		
				case OZCStringResource.IDS_OPT_VIEW_DETAIL:
					return "상세보기";		
				case OZCStringResource.IDS_OPT_DETAIL_MSG_COLON:
					return "상세메시지:";		
				case OZCStringResource.IDS_OPT_TEXT_CONTROL:
					return "텍스트 조정";							
				case OZCStringResource.IDS_OPT_SHRINK_TO_FIT:
					return "셀에 맞춤";						
				case OZCStringResource.IDS_OPT_PASSWORD2:
					return "확인";	
				case OZCStringResource.IDS_OPT_DM_BARCODE:
					return "DataMatrix 바코드";		
				case OZCStringResource.IDS_OPT_FONT_STYLE_COLON:
					return "글꼴 스타일 :";
				case OZCStringResource.IDS_OPT_COLOR_COLON:
					return "색 :";
				case OZCStringResource.IDS_OPT_INTERVAL_COLON:
					return "자간 :";
				case OZCStringResource.IDS_OPT_STRETCH_COLON:
					return "장평 :";
				case OZCStringResource.IDS_OPT_EFFECT:
					return "효과";	
				case OZCStringResource.IDS_OPT_UNDERLINE:
					return "밑줄";
				case OZCStringResource.IDS_OPT_STRIKEOUT:
					return "취소선";					
				case OZCStringResource.IDS_OPT_DOUBLE_STRIKEOUT:
					return "이중 취소선";	
				case OZCStringResource.IDS_OPT_NO_FILL:
					return "채우기 없음";
				case OZCStringResource.IDS_OPT_TRANSPARENCY:
					return "투명도";	
				case OZCStringResource.IDS_OPT_GRADIENT:
					return "그라데이션";	
				case OZCStringResource.IDS_OPT_USE_GRADIENT:
					return "그라데이션 사용";	
				case OZCStringResource.IDS_OPT_SLASH:
					return "상향 대각선";
				case OZCStringResource.IDS_OPT_BACK_SLASH:
					return "하향 대각선";	
				case OZCStringResource.IDS_OPT_START_POSITION:
					return "시작 위치";	
				case OZCStringResource.IDS_OPT_FROM_EDGE:
					return "모서리에서";	
				case OZCStringResource.IDS_OPT_DIRECT:
					return "정방향";	
				case OZCStringResource.IDS_OPT_FROM_CENTER:
					return "가운데에서";	
				case OZCStringResource.IDS_OPT_REVERSE:
					return "역방향";	
				case OZCStringResource.IDS_OPT_THICKNESS_W:
					return "두께(W) :";	
				case OZCStringResource.IDS_OPT_STYLE_Y:
					return "스타일(Y) :";	
				case OZCStringResource.IDS_OPT_COLOR_C:
					return "색(C) :";		
				case OZCStringResource.IDS_OPT_CLIPPING:
					return "클리핑";	
				case OZCStringResource.IDS_OPT_WORD_WRAP:
					return "자동 줄바꾸기";	
				case OZCStringResource.IDS_OPT_WORD_WRAP_TYPE:
					return "자동 줄바꾸기 타입";	
				case OZCStringResource.IDS_OPT_IGNORE_SPACE:
					return "공백 무시";	
				case OZCStringResource.IDS_OPT_NON_ASCII_WORD:
					return "단어 잘림 방지";	
				case OZCStringResource.IDS_OPT_TOOLTIP_TEXT:
					return "툴팁 텍스트...";		
				case OZCStringResource.IDS_OPT_BASIC:
					return "Basic";
				case OZCStringResource.IDS_OPT_OUTLINE:
					return "Outline";
				case OZCStringResource.IDS_OPT_3D:
					return "3D";
				case OZCStringResource.IDS_OPT_ENGRAVE:
					return "Engrave";
				case OZCStringResource.IDS_OPT_SEGMENT:
					return "Segments";
				case OZCStringResource.IDS_OPT_HOLLOW:
					return "Hollow";
				case OZCStringResource.IDS_OPT_SHADOW:
					return "Shadow";
				case OZCStringResource.IDS_OPT_IMAGE_STYLE:
					return "스타일 :";
				case OZCStringResource.IDS_OPT_BARCODE_STYLE:
					return "바코드 타입 :";
				case OZCStringResource.IDS_OPT_DATA_GAP:
					return "데이터 간격 :";
				case OZCStringResource.IDS_OPT_DATA_POSITION:
					return "데이터 위치 :";
				case OZCStringResource.IDS_OPT_BARCODE_MARGIN:
					return "바코드 여백 :";	
				case OZCStringResource.IDS_OPT_ALIGN_HORIZONTALLY:
					return "바코드 수평 정렬 :";
				case OZCStringResource.IDS_OPT_RATION_COLON:
					return "비율 :";
				case OZCStringResource.IDS_OPT_PRINTING_SCALE:
					return "프린트 크기 :";
				case OZCStringResource.IDS_OPT_ROTATION_COLON:
					return "회전 :";		
				case OZCStringResource.IDS_OPT_TITLE_GAP:
					return "제목 간격 :";
				case OZCStringResource.IDS_OPT_TITLE_POSITION:
					return "제목 위치 :";
				case OZCStringResource.IDS_OPT_BARCODE_ERROR_OPTION:
					return "에러 표시 옵션 :";
				case OZCStringResource.IDS_OPT_DISPLAY_ERROR_DATA:
					return "에러 데이터 표시";		
				case OZCStringResource.IDS_OPT_DISPLAY_ERROR_SIZE:
					return "바코드 크기 오류 표시";		
				case OZCStringResource.IDS_OPT_DISPLAY_EMPTY_DATA:
					return "빈 값 표시";	
				case OZCStringResource.IDS_OPT_PDF_IMAGE_STYLE:
					return "이미지 스타일 :";		
				case OZCStringResource.IDS_OPT_PDF_ROW_NUMBER:
					return "행수 :";		
				case OZCStringResource.IDS_OPT_PDF_COL_NUMBER:
					return "열수 :";		
				case OZCStringResource.IDS_OPT_PDF_ECC:
					return "에러교정 :";							
				case OZCStringResource.IDS_OPT_PDF_X_SCALE:
					return "X크기 :";		
				case OZCStringResource.IDS_OPT_PDF_Y_SCALE:
					return "Y크기 :";	
				case OZCStringResource.IDS_OPT_BORDER_COLOR:
					return "테두리 색 :";	
				case OZCStringResource.IDS_OPT_FILL_COLOR:
					return "바탕색 :";	
				case OZCStringResource.IDS_OPT_THICKNESS:
					return "두께 :";	
				case OZCStringResource.IDS_OPT_SHAPE_NO_FILL:
					return "투명 :";	
				case OZCStringResource.IDS_OPT_LINE_TYPE:
					return "선 종류 :";	
				case OZCStringResource.IDS_OPT_TOOLTIP_TEXT_COLON:
					return "툴팁 텍스트 :";	
				case OZCStringResource.IDS_OPT_EDIT_CHART:
					return "차트 편집";	
				case OZCStringResource.IDS_OPT_SHOW_DATA:
					return "데이터 보기";		
				case OZCStringResource.IDS_OPT_EDIT_SHAPE:
					return "도형편집";		
				case OZCStringResource.IDS_FORCS_RV:
					return "Forcs(R) OZ Report Viewer";							
				case OZCStringResource.IDS_OPT_EXCEL_LINE:
					return "라인";
				case OZCStringResource.IDS_OPT_EXCEL_SAVE_TYPE:
					return "저장 형태";		
				case OZCStringResource.IDS_OPT_JPG_ZOOM_INOUT:
					return "확대/축소 비율";						
				case OZCStringResource.IDS_OPT_ZOOM_INOUT_COLON:
					return "확대/축소 비율 : ";
				case OZCStringResource.IDS_OPT_QUALITY_COLON:
					return "품질 : ";		
				case OZCStringResource.IDS_OPT_TIFF_SIZE:
					return "크기";
				case OZCStringResource.IDS_OPT_PAGELINE:
					return "Line";
				case OZCStringResource.IDS_OPT_WORD_BAND_SAVE_PAGE:
					return "페이지 헤더/풋터 밴드를 머리글/바닥글에 저장";		
				case OZCStringResource.IDS_OPT_SAVE_OPTION:
					return "저장 방식";		
				case OZCStringResource.IDS_OPT_PRINT_MULTIDOC:
					return "다중문서";					
				case OZCStringResource.ID_PAGE_CONTROL_WINDOW:
					return "페이지 선택 창";					
				case OZCStringResource.IDS_OPT_FONT_NAME_COLON:
					return "글꼴 :";
				case OZCStringResource.IDS_OPT_FONT_OPTION:
					return "옵션";
				case OZCStringResource.IDS_OPT_AUTO_SELECTED:
					return "자동 맞춤";					
				case OZCStringResource.IDS_OPT_SIDE:
					return "너비";
				case OZCStringResource.IDS_OPT_LENGTH:
					return "높이";					
				case OZCStringResource.IDS_OPT_OTHER:
					return "사용자 정의";					
				case OZCStringResource.IDS_OPT_FONT_SAVE_WAY:
					return "글꼴";					
				case OZCStringResource.IDS_OPT_HORIZONTAL_NAVI:
					return "수평";										
				case OZCStringResource.IDS_OPT_VERTICAL_NAVI:
					return "수직";						
				case OZCStringResource.IDS_OPT_PNG_SAVE:
					return "PNG 저장 옵션";
				case OZCStringResource.IDS_OPT_GIF_SAVE:
					return "GIF 저장 옵션";		
				case OZCStringResource.IDS_OPT_REMOVE_LINES:
					return "라인 제거";		
				case OZCStringResource.IDS_OPT_ENCODING_HDM:
					return "인코딩 :";				
				case OZCStringResource.IDS_OPT_SVG_SAVE:
					return "SVG 저장 옵션";	
				case OZCStringResource.IDS_OPT_PDF_CHANGEABLE:
					return "변경 허용";	
				case OZCStringResource.IDS_OPT_PDF_RADIO_CHANGEABLE1:
					return "없음";	
				case OZCStringResource.IDS_OPT_PDF_RADIO_CHANGEABLE2:
					return "주석 달기, 양식 필드 채우기 및 서명";	
				case OZCStringResource.IDS_OPT_PDF_RADIO_CHANGEABLE3:
					return "페이지 레이아웃, 양식 필드 채우기 및 서명";	
				case OZCStringResource.IDS_OPT_PDF_RADIO_CHANGEABLE4:
					return "페이지 추출을 제외한 모두";	
				case OZCStringResource.IDS_OPT_SAVE_WAY_BASE:
					return "저장 방식";		
				case OZCStringResource.IDS_OPT_SAVE_LARGEBUNDLE_BYPAGE:
					return "보고서를 한 페이지/페이지별 로 바인딩한 후 저장합니다.";						
				case OZCStringResource.IDS_OPT_SAVE_LARGE_BUNDLE:
					return "한페이지로 저장";		
				case OZCStringResource.IDS_OPT_SAVE_BY_PAGE:
					return "페이지별로 저장";	
				case OZCStringResource.IDS_OPT_KEEP_SAVING:
					return "저장을 계속합니다.";			
				case OZCStringResource.IDC_TITLE_SECURITY:
					return "보호";		
				case OZCStringResource.IDC_EDIT_PASSWORD:
					return "암호";	
				case OZCStringResource.IDC_EDIT_PASSWORD_VERIFY:
					return "암호 확인";	
				case OZCStringResource.IDC_ZIP_GROUP:
					return "압축";	
				case OZCStringResource.IDC_COMPRESS_ON_SAVE:
					return "압축 저장";			
				case OZCStringResource.IDS_OPT_MARGIN_COLON:
					return "여백 :";	
				case OZCStringResource.IDC_PAPER_SETUP:
					return "용지 설정";					
				case OZCStringResource.IDC_CHECK_ADJUST:
					return "용지에 맞춰 인쇄";							

				case OZCStringResource.IDS_CHECK_EXCEL_ZOOM:
					return "배율";
				case OZCStringResource.IDS_CHECK_EXCEL_ZOOM_RATIO2:
					return "확대/축소 비율(%)";					
				case OZCStringResource.IDS_CHECK_EXCEL_ZOOM_RATIO:
					return "확대/축소 비율: ";							
				case OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES:
					return "틀 고정";							
				case OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES_ROW:
					return "행: ";							
				case OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES_COL:
					return "열: ";							
				case OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES_ROW_M:
					return "행 ";							
				case OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES_COL_M:
					return "열 ";					
				
			}
		} else if(CLocale.GetBaseLang_Flash().localeCompare("ja-JP") == 0) {
			switch (id) {
				case OZCStringResource.IDS_OPT_SAVEAS_DEFAULT:
					return "基本設定";
				case OZCStringResource.IDS_OPT_SAVEAS:
					return "レポートを保存";
				case OZCStringResource.IDS_OPT_SAVE_WAY_TXT:
					return "Save report after binding per one page/pages.";
				case OZCStringResource.IDS_OPT_SAVE_WAY_ONEPAGE:
					return "Save Per OnePage";
				case OZCStringResource.IDS_OPT_SAVE_WAY_EACHPAGE:
					return "Save Per Pages";	

				case OZCStringResource.IDS_OPT_SAVE_WAY:
					return "保存方式";
				case OZCStringResource.IDS_OPT_SAVE_ACTION:
					return "Action";
				case OZCStringResource.IDS_OPT_SAVE_MULTIDOC:
					return "マルチ レポート";
				case OZCStringResource.IDS_OPT_SAVE_AREA:
					return "保存ページの範囲";
				case OZCStringResource.IDS_OPT_SAVE_COMPONENT:
					return "コンポーネント";
				case OZCStringResource.IDS_OPT_SAVE_LINK_INFO:
					return "リンク情報の保存";				
				case OZCStringResource.IDS_OPT_ACTION_AFTER_SAVE:
					return "Action after save";					
				case OZCStringResource.IDS_OPT_SAVE_ONEFILE:
					return "１つのファイルで保存";					
				case OZCStringResource.IDS_OPT_SAVE_ALLFILE:
					return "全てのレポートを保存";		
				case OZCStringResource.IDS_OPT_ALLPAGE:
					return "すべて";					
				case OZCStringResource.IDS_OPT_CURRENTPAGE:
					return "現在のページ";					
				case OZCStringResource.IDS_OPT_SELECTEDPAGE:
					return "選択したページ";		
				case OZCStringResource.IDS_OPT_CHOOSEPAGE:
					return "ページ指定 : ";
				case OZCStringResource.IDS_OPT_CHOOSEPAGE_DETAIL:
					return "ページ番号をカンマで区切って指定するか、ページ範囲を指定してください。";
				case OZCStringResource.IDS_OPT_CHOOSEPAGE_DETAIL2:
					return "例）1,3,5-12";
				case OZCStringResource.IDS_OPT_COMPONENT_LABEL:
					return "保存するコンポーネントをチェックしてください";					
				case OZCStringResource.IDS_OPT_LABEL:
					return "ラベル";
				case OZCStringResource.IDS_OPT_IMAGE:
					return "イメージ";
				case OZCStringResource.IDS_OPT_BARCODE:
					return "バーコード";					
				case OZCStringResource.IDS_OPT_PDF417:
					return "PDF417コード";					
				case OZCStringResource.IDS_OPT_QR_BARCODE:
					return "QRコード";					
				case OZCStringResource.IDS_OPT_HTML:
					return "HTML";	
				case OZCStringResource.IDS_OPT_CHART:
					return "チャート";			
				case OZCStringResource.IDS_OPT_LINE:
					return "直線";			
				case OZCStringResource.IDS_OPT_CIRCLE:
					return "楕円";			
				case OZCStringResource.IDS_OPT_RECTANGLE:
					return "長方形";			
				case OZCStringResource.IDS_OPT_ARROW:
					return "矢印";			
				case OZCStringResource.IDS_OPT_URL_LINK:
					return "URLリンク";			
				case OZCStringResource.IDS_OPT_LIST_LINK:
					return "目次リンク";
				case OZCStringResource.IDS_OPT_PDF_SAVE:
					return "PDF保存の設定";		
				case OZCStringResource.IDS_OPT_DOC_IMFOR:
					return "文書情報";			
				case OZCStringResource.IDS_OPT_OPEN_PROTECT:
					return "読み取りパスワード";					
				case OZCStringResource.IDS_OPT_EDIT_PROTECT:
					return "書き込みパスワード";
				case OZCStringResource.IDS_OPT_USER_RIGHT:
					return "ユーザーのアクセス権限";							
				case OZCStringResource.IDS_OPT_TITLE:
					return "タイトル";
				case OZCStringResource.IDS_OPT_WRITER:
					return "作成者";			
				case OZCStringResource.IDS_OPT_SUBJECT:
					return "サブ タイトル";			
				case OZCStringResource.IDS_OPT_KEYWORD:
					return "キーワード";
				case OZCStringResource.IDS_OPT_APPLICATION:
					return "アプリケーション";			
				case OZCStringResource.IDS_OPT_USER_PW:
					return "パスワード";
				case OZCStringResource.IDS_OPT_USER_PW_CONFIRM:
					return "パスワードの確認";						
				case OZCStringResource.IDS_OPT_MASTER_PW:
					return "パスワード";
				case OZCStringResource.IDS_OPT_MASTER_PW_CONFIRM:
					return "パスワードの確認";		
				case OZCStringResource.IDS_OPT_PRINT_PERMIT:
					return "印刷可能";
				case OZCStringResource.IDS_OPT_CONTENTS_COPY:
					return "内容のコピー";
				case OZCStringResource.IDS_OPT_SAVEIMG_TOCHART:
					return "チャートをイメージで保存";
				case OZCStringResource.IDS_OPT_FONT_INCLUDE:
					return "폰트 포함";

				case OZCStringResource.IDS_OPT_PRINT:
					return "印刷";
				case OZCStringResource.IDS_OPT_PRINT_COUNT:
					return "印刷枚数";		
				case OZCStringResource.IDS_OPT_PRINT_SCOPE:
					return "印刷範囲";
				case OZCStringResource.IDS_OPT_PRINT_RULE:
					return "印刷方式";
				case OZCStringResource.IDS_OPT_COUNT:
					return "枚";
				case OZCStringResource.IDS_OPT_PRINT_ONEPAGE:
					return "一部ずつ印刷";
				case OZCStringResource.IDS_OPT_PRINT_ALL_REPORT:
					return "すべての文書を印刷";					
				case OZCStringResource.IDS_OPT_ALL:
					return "すべて";					
				case OZCStringResource.IDS_OPT_CHOOSE_PAGE:
					return "ページ指定";	
				case OZCStringResource.IDS_OPT_PRINT_FITTOPAGE:
					return "用紙にあわせて印刷";				
				case OZCStringResource.IDS_OPT_DEVIDE_PRINT:
					return "分割印刷";
				case OZCStringResource.IDS_OPT_DEVIDE_PRINT_TOFITPAPER:
					return "用紙に合わせて分割印刷";					
				case OZCStringResource.IDS_TOGETHER_PRINT:
					return "割付け印刷";					
				case OZCStringResource.IDS_OPT_PAGECOUNT_TOPRINT:
					return "割付けページ数";	
				case OZCStringResource.IDS_OPT_PRINT_ORDER:
					return "印刷順序";										
				case OZCStringResource.IDS_OPT_HORIZON:
					return "左右";										
				case OZCStringResource.IDS_OPT_VERTICAL:
					return "上下";										
				case OZCStringResource.IDS_OPT_PRINT_DIRECTION:
					return "印刷方向";										
				case OZCStringResource.IDS_OPT_PORTRAIT:
					return "縦";										
				case OZCStringResource.IDS_OPT_LANDSCAPE:
					return "横";						
				case OZCStringResource.IDS_OPT_OPTION:
					return "設定";		
				case OZCStringResource.IDS_OPT_CONFIRM:
					return "OK";		
				case OZCStringResource.IDS_OPT_CANCEL:
					return "キャンセル";							
				case OZCStringResource.IDS_OPT_SAVE:
					return "저장";						
				case OZCStringResource.IDS_OPT_OZD_SAVE_OPTION:
					return "OZD保存の設定";								
				case OZCStringResource.IDS_OPT_PASSWORD:
					return "パスワード";		
				case OZCStringResource.IDS_OPT_MEMOADD_OK:
					return "編集内容を含む";		
				case OZCStringResource.IDS_OPT_SAVE_ALLREPORT:
					return "複数レポートの保存";						
				case OZCStringResource.IDS_OPT_INCLUDE_IMG:
					return "イメージを含む";						
				case OZCStringResource.IDS_OPT_FILE_TYPE:
					return "ファイルの種類";			
				case OZCStringResource.IDS_OPT_EXCEL_SAVE:
					return "Excel保存の設定";
				case OZCStringResource.IDS_OPT_EXCEL_SAVE2:
					return "Excel保存の設定(詳細設定)";					
				case OZCStringResource.IDS_OPT_NEXCEL_SAVE:
					return "Nexcel保存の設定";
				case OZCStringResource.IDS_OPT_WORD_SAVE:
					return "Word保存の設定";						
				case OZCStringResource.IDS_OPT_PPT_SAVE:
					return "PowerPoint保存の設定";		
				case OZCStringResource.IDS_OPT_HTML_SAVE:
					return "HTML保存の設定";		
				case OZCStringResource.IDS_OPT_CSV_SAVE:
					return "CSV保存の設定";		
				case OZCStringResource.IDS_OPT_TEXT_SAVE:
					return "TEXT保存の設定";		
				case OZCStringResource.IDS_OPT_JPG_SAVE:
					return "JPG保存の設定";
				case OZCStringResource.IDS_OPT_TIFF_SAVE:
					return "TIFF保存の設定";		
				case OZCStringResource.IDS_OPT_HWP_SAVE:
					return "HWP保存の設定";		
				case OZCStringResource.IDS_OPT_HWP97_SAVE:
					return "HWP97保存の設定";		
				case OZCStringResource.IDS_OPT_MHT_SAVE:
					return "MHT保存の設定";
				case OZCStringResource.IDS_OPT_SAVE_FORM:
					return "保存形式";							
				case OZCStringResource.IDS_OPT_PAGE:
					return "ページ";
				case OZCStringResource.IDS_OPT_FONT:
					return "フォント";
				case OZCStringResource.IDS_OPT_FORMAT:
					return "書式";
				case OZCStringResource.IDS_OPT_AUTO_FIT:
					return "次のページ数に合わせる";
				case OZCStringResource.IDS_OPT_CELL_MINIMUM_SIZE_CELL:
					return "セルの最小サイズ";
				case OZCStringResource.IDS_OPT_SAVE_MODE:
					return "保存形式";			
				case OZCStringResource.IDS_OPT_KEEP_FORM_ATTR:
					return "レポートタイプの保持";			
				case OZCStringResource.IDS_OPT_IGNORE_COLOR_OUTLINE_CELL:
					return "色、枠線、セル結合の無視";
				case OZCStringResource.IDS_OPT_REMOVE_CELL_AFTERDIVIDE:
					return "分割後空白セルを削除";
				case OZCStringResource.IDS_OPT_SAVE_SHEET_PER_COLPAGE:
					return "ページの列ごとにシートに保存";
				case OZCStringResource.IDS_OPT_SAVE_SHEET_PAGE:
					return "ページごとにシートに保存";
				case OZCStringResource.IDS_OPT_COLUMN_FIRST:
					return "列を優先して保存";
				case OZCStringResource.IDS_OPT_ROW_FIRST:
					return "行を優先して保存";			
				case OZCStringResource.IDS_OPT_REMOVE_PAGE_GAP:
					return "ページ間の空白の削除";
				case OZCStringResource.IDS_OPT_SIZE_COLON:
					return "サイズ:";
				case OZCStringResource.IDS_OPT_REMOVE_LINE:
					return "行の削除";		
				case OZCStringResource.IDS_OPT_EXCLUDE_FIRST_PAGE:
					return "先頭ページ除外";
				case OZCStringResource.IDS_OPT_ONLY_FIRST_PAGE:
					return "先頭ページのみ";
				case OZCStringResource.IDS_OPT_ALL_PAGE:
					return "すべて";						
				case OZCStringResource.IDS_OPT_LINE_SCOPE_LINE_NUMBER:
					return "行の範囲を(-)で、行番号をカンマ(,)またはスラッシュ(/)で区切って指定してください。例）1-3,5,12";					
				case OZCStringResource.IDS_OPT_CHANGE_TXT_TO_INTEGER:
					return "数値を数字に変換";
				case OZCStringResource.IDS_OPT_APPLY_ONLYLABEL_NUMBER:
					return "表示形式が数字のラベルのみ適用";
				case OZCStringResource.IDS_OPT_CHANGE_AS_NORMAL:
					return "セルの書式設定を標準に変換";						
				case OZCStringResource.IDS_OPT_FONT_COLON:
					return "フォント:";					
				case OZCStringResource.IDS_OPT_WIDTH:
					return "幅";
				case OZCStringResource.IDS_OPT_HEIGHT:
					return "高さ";
				case OZCStringResource.IDS_OPT_SAVE_MHT_TYPE:
					return "mht形式で保存 （Office XP以降対応）";					
				case OZCStringResource.IDS_OPT_TABLE_SAVE_WAY:
					return "テーブルの保存方式";
				case OZCStringResource.IDS_OPT_TABLE_TYPE_WAY:
					return "テーブル形式で保存           ";
				case OZCStringResource.IDS_OPT_BAND_SAVE_WAY:
					return "バンド保存方式";
				case OZCStringResource.IDS_OPT_BAND_SAVE_PAGE:
					return "ページ ヘッダー/フッター バンドをヘッダー/フッターに保存";		
				case OZCStringResource.IDS_OPT_PPT_SAVE_WAY:
					return "PowerPoint保存方式";
				case OZCStringResource.IDS_OPT_MHTD_SAVE_PAGE:
					return "mht形式で保存（Office XP以降対応）";		
				case OZCStringResource.IDS_OPT_FONT_SIZE_SETTING:
					return "フォント サイズ";
				case OZCStringResource.IDS_OPT_PAGE_BETWEEN:
					return "ページの間隔";
				case OZCStringResource.IDS_OPT_PER_PAGE_SAVE:
					return "ページごとに保存";								
				case OZCStringResource.IDS_OPT_LINE_COUNT_COLON:
					return "ページ間の余白(ピクセル)";	
				case OZCStringResource.IDS_OPT_OFFSET_COLON:
					return "オフセット :";
				case OZCStringResource.IDS_OPT_STARTPOINT_HTMLDOC:
					return "HTML開始座標";					
				case OZCStringResource.IDS_OPT_ENCODING_COLON:
					return "エンコード方式 :";					
				case OZCStringResource.IDS_OPT_X_AXIS:
					return "X軸";							
				case OZCStringResource.IDS_OPT_Y_AXIS:
					return "Y軸";	
				case OZCStringResource.IDS_OPT_OFFSET:
					return "オフセット";
				case OZCStringResource.IDS_OPT_SEPARATION:
					return "文字の区切り";							
				case OZCStringResource.IDS_OPT_SEPARATOR:
					return "区切り文字";	
				case OZCStringResource.IDS_OPT_USER_DEFINE:
					return "ユーザー定義";						
				case OZCStringResource.IDS_OPT_SEPARATION_TYPE:
					return "区切り形式";
				case OZCStringResource.IDS_OPT_PAGE_BRACKET:
					return "<PAGE>";
				case OZCStringResource.IDS_OPT_SET_PAGELINE:
					return "ページ間の行数";
				case OZCStringResource.IDS_OPT_CHAR_NUMBER:
					return "数字";					
				case OZCStringResource.IDS_OPT_ADD_SEPARATOR:
					return "桁数の区切り文字を追加";					
				case OZCStringResource.IDS_OPT_REMOVE_SEPARATOR:
					return "桁数の区切り文字を削除";			
				case OZCStringResource.IDS_OPT_PAGE_SEPARATION:
					return "ページの区切り";						
				case OZCStringResource.IDS_OPT_LABEL_SAVE:
					return "ラベルの保存方式";		
				case OZCStringResource.IDS_OPT_LABEL_SIZE:
					return "ラベルサイズ固定";
				case OZCStringResource.IDS_OPT_POSITION_SAVE:
					return "位置の保存方式";
				case OZCStringResource.IDS_OPT_VERTREL_TOPARA:
					return "縦を文段に設定";
				case OZCStringResource.IDS_OPT_BACKGROUND_BAND:
					return "バックグラウンドバンドをグラウンド側に保存";	
				case OZCStringResource.IDS_OPT_TITLE_COLON:
					return "タイトル  :";			
				case OZCStringResource.IDS_OPT_DISPLAY_OPTION:
					return "表示オプション";	
				case OZCStringResource.IDS_OPT_GRIDLINES:
					return "枠線を表示する";			
				case OZCStringResource.IDS_OPT_ENCODING_WAY:
					return "エンコード方式";
				case OZCStringResource.IDS_OPT_ENCODING_G3:
					return "G3";	
				case OZCStringResource.IDS_OPT_ENCODING_G4:
					return "G4";		
				case OZCStringResource.IDS_OPT_ENCODING_SAVEAS_ONEFILE:
					return "１つのファイルに保存";
				case OZCStringResource.IDS_OPT_RATIO:
					return "比率";	
				case OZCStringResource.IDS_OPT_ZOOM_INOUT:
					return "拡大/縮小比率";
				case OZCStringResource.IDS_OPT_SET_DPI:
					return "解像度によってサイズ決定";  
				case OZCStringResource.IDS_OPT_DPI:
					return "DPI";	
				case OZCStringResource.IDS_OPT_SIZE:
					return "サイズ";	
				case OZCStringResource.IDS_OPT_SAVE_RATIO:
					return "保存倍率";
				case OZCStringResource.IDS_OPT_COMPRESSION:
					return "圧縮";
				case OZCStringResource.IDS_OPT_QUALITY:
					return "品質";					
				case OZCStringResource.IDS_OPT_USE_INDENT:
					return "インデント";
				case OZCStringResource.IDS_OPT_DATA_SAVE:
					return "データ保存";
				case OZCStringResource.IDS_OPT_FIND_WHAT:
					return "検索する内容：";	
				case OZCStringResource.IDS_OPT_MATCH_WHOLE_WORD:
					return "単語単位で探す";
				case OZCStringResource.IDS_OPT_MATCH_CASE:
					return "大文字と小文字を区別する";
				case OZCStringResource.IDS_OPT_DIRECTION:
					return "方向";			
				case OZCStringResource.IDS_OPT_SEARCH_UP:
					return "上へ";
				case OZCStringResource.IDS_OPT_SEARCH_DOWN:
					return "下へ";
				case OZCStringResource.IDS_OPT_NEXT:
					return "次を検索";						
				case OZCStringResource.IDS_OPT_REFRESH_REPORT:
					return "更新";		
				case OZCStringResource.IDS_OPT_INTERVAL:
					return "周期";	
				case OZCStringResource.IDS_OPT_HH:
					return "時";		
				case OZCStringResource.IDS_OPT_MM:
					return "分";		
				case OZCStringResource.IDS_OPT_SS:
					return "秒";	
				case OZCStringResource.IDS_OPT_REFRESH_NOW:
					return "更新";		
				case OZCStringResource.IDS_OPT_START:
					return "開始";		
				case OZCStringResource.IDS_OPT_STOP:
					return "終了";
				case OZCStringResource.IDS_OPT_LEFT_COLON:
					return "左:";
				case OZCStringResource.IDS_OPT_RIGHT_COLON:
					return "右:";
				case OZCStringResource.IDS_OPT_TOP_COLON:
					return "上:";
				case OZCStringResource.IDS_OPT_BOTTOM_COLON:
					return "下:";	
				case OZCStringResource.IDS_OPT_PAGE_SETUP:
					return "ページ設定";	
				case OZCStringResource.IDS_OPT_MARGIN:
					return "余白";	
				case OZCStringResource.IDS_OPT_UNIT:
					return "使用単位";		
				case OZCStringResource.IDS_OPT_UNIT_COLON:
					return "単位：";	
				case OZCStringResource.IDS_OPT_MARGIN_OPTION:
					return "余白オプション";		
				case OZCStringResource.IDS_OPT_MARGIN_DEFAULT:
					return "基本余白使用";	
				case OZCStringResource.IDS_OPT_MARGIN_AUTO_ADJUST:
					return "自動調整";	
				case OZCStringResource.IDS_OPT_HTML_VERSION:
					return "バージョン";		
				case OZCStringResource.IDS_OPT_HTML_VERSION_COLON:
					return "HTML バージョン :";					
				case OZCStringResource.IDS_OPT_TOTAL:
					return "全体";	
				case OZCStringResource.IDS_OPT_REPORT_STRUCTURE:
					return "レポートの構造表示";
				case OZCStringResource.IDS_OPT_LABEL_FORMAT:
					return "ラベル書式";	
				case OZCStringResource.IDS_OPT_PARAGRAPH:
					return "段落";	
				case OZCStringResource.IDS_OPT_FILL:
					return "塗りつぶし";	
				case OZCStringResource.IDS_OPT_LINECOLOR:
					return "枠線";	
				case OZCStringResource.IDS_OPT_ETC:
					return "その他";			
				case OZCStringResource.IDS_OPT_ALIGNMENT:
					return "文字の配置";						
				case OZCStringResource.IDS_OPT_TXT_DIRECTION:
					return "テキスト方向";	
				case OZCStringResource.IDS_OPT_INTERNAL_MARGIN:
					return "内部余白";	
				case OZCStringResource.IDS_OPT_PREVIEW:
					return "プレビュー";	
				case OZCStringResource.IDS_OPT_HORIZON_COLON:
					return "左右：";										
				case OZCStringResource.IDS_OPT_VERTICAL_COLON:
					return "上下：";	
				case OZCStringResource.IDS_OPT_LINE_SPACE_COLON:
					return "行間：";										
				case OZCStringResource.IDS_OPT_INDENT_COLON:
					return "インデント：";		
				case OZCStringResource.ID_OPT_PARAMETERS_TITLE_LABEL:
					return "パラメーター値を修正します。";
				case OZCStringResource.IDS_OPT_EXIT:
					return "終了";				
				case OZCStringResource.IDS_OPT_OZZPASSWORD:
					return "パスワード";		
				case OZCStringResource.IDS_OPT_OZZPASSWORD_CHECK:
					return "パスワード確認";		
				case OZCStringResource.IDS_OPT_ID:
					return "ID";		
				case OZCStringResource.IDS_OPT_SECURITY:
					return "セキュレティー";
				case OZCStringResource.IDS_OPT_PRODUCT_INFO:
					return "製品情報";
				case OZCStringResource.IDS_OPT_OZ_INFO:
					return "製品 情報";			
				case OZCStringResource.IDS_OPT_ERROR_CODE_COLON:
					return "エラー コード:";			
				case OZCStringResource.IDS_OPT_GENERAL_MSG_COLON:
					return "一般メッセージ：";		
				case OZCStringResource.IDS_OPT_VIEW_DETAIL:
					return "詳細プレビュー";		
				case OZCStringResource.IDS_OPT_DETAIL_MSG_COLON:
					return "詳細メッセージ：";			
				case OZCStringResource.IDS_OPT_TEXT_CONTROL:
					return "文字の制御";							
				case OZCStringResource.IDS_OPT_SHRINK_TO_FIT:
					return "縮小して全体を表示する";		
				case OZCStringResource.IDS_OPT_PASSWORD2:
					return "パスワードの確認";	
				case OZCStringResource.IDS_OPT_DM_BARCODE:
					return "DataMatrixコード";		
				case OZCStringResource.IDS_OPT_FONT_STYLE_COLON:
					return "フォント スタイル：";
				case OZCStringResource.IDS_OPT_COLOR_COLON:
					return "色：";
				case OZCStringResource.IDS_OPT_INTERVAL_COLON:
					return "字間：";					
				case OZCStringResource.IDS_OPT_STRETCH_COLON:
					return "Stretch :";					
				case OZCStringResource.IDS_OPT_EFFECT:
					return "飾り";	
				case OZCStringResource.IDS_OPT_UNDERLINE:
					return "下線";					
				case OZCStringResource.IDS_OPT_STRIKEOUT:
					return "取り消し線";					
				case OZCStringResource.IDS_OPT_DOUBLE_STRIKEOUT:
					return "二重取り消し線";
				case OZCStringResource.IDS_OPT_NO_FILL:
					return "塗りつぶしなし";
				case OZCStringResource.IDS_OPT_TRANSPARENCY:
					return "透過度";	
				case OZCStringResource.IDS_OPT_GRADIENT:
					return "グラデーション";	
				case OZCStringResource.IDS_OPT_USE_GRADIENT:
					return "グラデーション使用";	
				case OZCStringResource.IDS_OPT_SLASH:
					return "スラッシュ";
				case OZCStringResource.IDS_OPT_BACK_SLASH:
					return "逆スラッシュ";	
				case OZCStringResource.IDS_OPT_START_POSITION:
					return "開始位置";	
				case OZCStringResource.IDS_OPT_FROM_EDGE:
					return "段";	
				case OZCStringResource.IDS_OPT_DIRECT:
					return "正方向";	
				case OZCStringResource.IDS_OPT_FROM_CENTER:
					return "中央";	
				case OZCStringResource.IDS_OPT_REVERSE:
					return "逆方向";					
				case OZCStringResource.IDS_OPT_THICKNESS_W:
					return "太さ(W) :";	
				case OZCStringResource.IDS_OPT_STYLE_Y:
					return "スタイル(Y) :";	
				case OZCStringResource.IDS_OPT_COLOR_C:
					return "色(C) :";		
				case OZCStringResource.IDS_OPT_CLIPPING:
					return "クリッピング";	
				case OZCStringResource.IDS_OPT_WORD_WRAP:
					return "自動改行";	
				case OZCStringResource.IDS_OPT_WORD_WRAP_TYPE:
					return "自動改行タイプ";	
				case OZCStringResource.IDS_OPT_IGNORE_SPACE:
					return "空白無視";	
				case OZCStringResource.IDS_OPT_NON_ASCII_WORD:
					return "単語の途中で改行を防止";	
				case OZCStringResource.IDS_OPT_TOOLTIP_TEXT:
					return "ツールヒント テキスト...";	
				case OZCStringResource.IDS_OPT_BASIC:
					return "Basic";
				case OZCStringResource.IDS_OPT_OUTLINE:
					return "Outline";
				case OZCStringResource.IDS_OPT_3D:
					return "3D";
				case OZCStringResource.IDS_OPT_ENGRAVE:
					return "Engrave";
				case OZCStringResource.IDS_OPT_SEGMENT:
					return "Segments";
				case OZCStringResource.IDS_OPT_HOLLOW:
					return "Hollow";
				case OZCStringResource.IDS_OPT_SHADOW:
					return "Shadow";						
				case OZCStringResource.IDS_OPT_IMAGE_STYLE:
					return "スタイル：";		
				case OZCStringResource.IDS_OPT_BARCODE_STYLE:
					return "スタイル ：";
				case OZCStringResource.IDS_OPT_DATA_GAP:
					return "データの間隔 ：";
				case OZCStringResource.IDS_OPT_DATA_POSITION:
					return "データの位置 ：";
				case OZCStringResource.IDS_OPT_BARCODE_MARGIN:
					return "バーコードの余白 ：";	
				case OZCStringResource.IDS_OPT_ALIGN_HORIZONTALLY:
					return "横方向揃え ：";
				case OZCStringResource.IDS_OPT_RATION_COLON:
					return "バーの比率 ：";
				case OZCStringResource.IDS_OPT_PRINTING_SCALE:
					return "印刷時、拡大/縮小 ：";
				case OZCStringResource.IDS_OPT_ROTATION_COLON:
					return "回転 ：";		
				case OZCStringResource.IDS_OPT_TITLE_GAP:
					return "タイトルの間隔 ：";
				case OZCStringResource.IDS_OPT_TITLE_POSITION:
					return "タイトルの位置 ：";
				case OZCStringResource.IDS_OPT_BARCODE_ERROR_OPTION:
					return "エラー表示オプション :";					
				case OZCStringResource.IDS_OPT_DISPLAY_ERROR_DATA:
					return "エラー データ表示";		
				case OZCStringResource.IDS_OPT_DISPLAY_ERROR_SIZE:
					return "バーコードのサイズ エラー表示";		
				case OZCStringResource.IDS_OPT_DISPLAY_EMPTY_DATA:
					return "空白値表示";
				case OZCStringResource.IDS_OPT_PDF_IMAGE_STYLE:
					return "イメージのスタイル：";		
				case OZCStringResource.IDS_OPT_PDF_ROW_NUMBER:
					return "行数：";		
				case OZCStringResource.IDS_OPT_PDF_COL_NUMBER:
					return "列数：";		
				case OZCStringResource.IDS_OPT_PDF_ECC:
					return "誤り訂正符号数：";							
				case OZCStringResource.IDS_OPT_PDF_X_SCALE:
					return "Xのサイズ：";		
				case OZCStringResource.IDS_OPT_PDF_Y_SCALE:
					return "Yのサイズ：";	
				case OZCStringResource.IDS_OPT_BORDER_COLOR:
					return "枠線色 :";	
				case OZCStringResource.IDS_OPT_FILL_COLOR:
					return "塗りつぶし色 :";	
				case OZCStringResource.IDS_OPT_THICKNESS:
					return "線の太さ :";	
				case OZCStringResource.IDS_OPT_SHAPE_NO_FILL:
					return "透過 :";	
				case OZCStringResource.IDS_OPT_LINE_TYPE:
					return "線のタイプ :";	
				case OZCStringResource.IDS_OPT_TOOLTIP_TEXT_COLON:
					return "ツールヒント テキスト :";	
				case OZCStringResource.IDS_OPT_EDIT_CHART:
					return "チャート編集";	
				case OZCStringResource.IDS_OPT_SHOW_DATA:
					return "データ表示";		
				case OZCStringResource.IDS_OPT_EDIT_SHAPE:
					return "図形編集";		
				case OZCStringResource.IDS_FORCS_RV:
					return "Forcs(R) OZ Report Viewer";							
				case OZCStringResource.IDS_OPT_EXCEL_LINE:
					return "行";						
				case OZCStringResource.IDS_OPT_EXCEL_SAVE_TYPE:
					return "保存形式";	
				case OZCStringResource.IDS_OPT_JPG_ZOOM_INOUT:
					return "拡大/縮小の比率 ";					
				case OZCStringResource.IDS_OPT_ZOOM_INOUT_COLON:
					return "拡大/縮小の比率 : ";
				case OZCStringResource.IDS_OPT_QUALITY_COLON:
					return "品質 : ";							
				case OZCStringResource.IDS_OPT_TIFF_SIZE:
					return "サイズ";		
				case OZCStringResource.IDS_OPT_PAGELINE:
					return "行";	
				case OZCStringResource.IDS_OPT_WORD_BAND_SAVE_PAGE:
					return "ページ ヘッダー/フッター バンドをヘッダー/フッターに保存";						
				case OZCStringResource.IDS_OPT_SAVE_OPTION:
					return "保存方式";						
				case OZCStringResource.IDS_OPT_PRINT_MULTIDOC:
					return "マルチ レポート";		
				case OZCStringResource.ID_PAGE_CONTROL_WINDOW:
					return "ページ選択";		
				case OZCStringResource.IDS_OPT_FONT_NAME_COLON:
					return "フォント：";
				case OZCStringResource.IDS_OPT_FONT_OPTION:
					return "オプション";
				case OZCStringResource.IDS_OPT_AUTO_SELECTED:
					return "次のページ数に合わせる";		
				case OZCStringResource.IDS_OPT_SIDE:
					return "横";
				case OZCStringResource.IDS_OPT_LENGTH:
					return "縦";		
				case OZCStringResource.IDS_OPT_OTHER:
					return "その他";	
				case OZCStringResource.IDS_OPT_FONT_SAVE_WAY:
					return "保存方式";	
				case OZCStringResource.IDS_OPT_HORIZONTAL_NAVI:
					return "水平";										
				case OZCStringResource.IDS_OPT_VERTICAL_NAVI:
					return "垂直";		
				case OZCStringResource.IDS_OPT_PNG_SAVE:
					return "PNG保存の設定";
				case OZCStringResource.IDS_OPT_GIF_SAVE:
					return "GIF保存の設定";	
				case OZCStringResource.IDS_OPT_REMOVE_LINES:
					return "行の削除";	
				case OZCStringResource.IDS_OPT_ENCODING_HDM:
					return "エンコード：";				
				case OZCStringResource.IDS_OPT_SVG_SAVE:
					return "SVG 保存の設定";	
				case OZCStringResource.IDS_OPT_PDF_CHANGEABLE:
					return "変更を許可";	
				case OZCStringResource.IDS_OPT_PDF_RADIO_CHANGEABLE1:
					return "なし";	
				case OZCStringResource.IDS_OPT_PDF_RADIO_CHANGEABLE2:
					return "注釈の作成、フォームフィールドの入力と既存の署名フィールドに署名";	
				case OZCStringResource.IDS_OPT_PDF_RADIO_CHANGEABLE3:
					return "ページレイアウト、フォームフィールドの入力と既存の署名フィールドに署名";	
				case OZCStringResource.IDS_OPT_PDF_RADIO_CHANGEABLE4:
					return "ページの抽出を除くすべての操作";	
				case OZCStringResource.IDS_OPT_SAVE_WAY_BASE:
					return "保存形式";		
				case OZCStringResource.IDS_OPT_SAVE_LARGEBUNDLE_BYPAGE:
					return "レポートを1ページでバインディングしてから保存し";						
				case OZCStringResource.IDS_OPT_SAVE_LARGE_BUNDLE:
					return "1ページで保存";		
				case OZCStringResource.IDS_OPT_SAVE_BY_PAGE:
					return "ページごとに保存";	
				case OZCStringResource.IDS_OPT_KEEP_SAVING:
					return "keep saving";	
				case OZCStringResource.IDC_TITLE_SECURITY:
					return "読み取りパスワード";		
				case OZCStringResource.IDC_EDIT_PASSWORD:
					return "パスワード";	
				case OZCStringResource.IDC_EDIT_PASSWORD_VERIFY:
					return "パスワードの確認";	
				case OZCStringResource.IDC_ZIP_GROUP:
					return "圧縮";	
				case OZCStringResource.IDC_COMPRESS_ON_SAVE:
					return "圧縮保存";
				case OZCStringResource.IDS_OPT_MARGIN_COLON:
					return "内部余白 :";	
				case OZCStringResource.IDC_PAPER_SETUP:
					return "用紙設定";					
				case OZCStringResource.IDC_CHECK_ADJUST:
					return "用紙に合わせて印刷";									

				case OZCStringResource.IDS_CHECK_EXCEL_ZOOM:
					return "比率";
				case OZCStringResource.IDS_CHECK_EXCEL_ZOOM_RATIO2:
					return "拡大/縮小比率(%)";					
				case OZCStringResource.IDS_CHECK_EXCEL_ZOOM_RATIO:
					return "拡大/縮小比率: ";							
				case OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES:
					return "ウィンドウ枠の固定";							
				case OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES_ROW:
					return "行: ";							
				case OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES_COL:
					return "列: ";		
				case OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES_ROW_M:
					return "行 ";							
				case OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES_COL_M:
					return "列 ";		
								
			}
		}
		if(CLocale.GetBaseLang_Flash().localeCompare("zh-CN") == 0) {
			switch (id) {
				case OZCStringResource.IDS_OPT_SAVEAS_DEFAULT:
					return "基本选项";
				case OZCStringResource.IDS_OPT_SAVEAS:
					return "保存选项";
				case OZCStringResource.IDS_OPT_SAVE_WAY_TXT:
					return "Save report after binding per one page/pages.";
				case OZCStringResource.IDS_OPT_SAVE_WAY_ONEPAGE:
					return "Save Per OnePage";
				case OZCStringResource.IDS_OPT_SAVE_WAY_EACHPAGE:
					return "Save Per Pages";	

				case OZCStringResource.IDS_OPT_SAVE_WAY:
					return "保存方式";
				case OZCStringResource.IDS_OPT_SAVE_ACTION:
					return "Action";
				case OZCStringResource.IDS_OPT_SAVE_MULTIDOC:
					return "多文档";
				case OZCStringResource.IDS_OPT_SAVE_AREA:
					return "保存范围";
				case OZCStringResource.IDS_OPT_SAVE_COMPONENT:
					return "组件";
				case OZCStringResource.IDS_OPT_SAVE_LINK_INFO:
					return "保存链接";	
				case OZCStringResource.IDS_OPT_ACTION_AFTER_SAVE:
					return "Action after save";					
				case OZCStringResource.IDS_OPT_SAVE_ONEFILE:
					return "单文档保存";					
				case OZCStringResource.IDS_OPT_SAVE_ALLFILE:
					return "多文档保存";		
				case OZCStringResource.IDS_OPT_ALLPAGE:
					return "全部";					
				case OZCStringResource.IDS_OPT_CURRENTPAGE:
					return "当前页";					
				case OZCStringResource.IDS_OPT_SELECTEDPAGE:
					return "选定页";		
				case OZCStringResource.IDS_OPT_CHOOSEPAGE:
					return "自定义 : ";	
				case OZCStringResource.IDS_OPT_CHOOSEPAGE_DETAIL:
					return "请键入页码和/或用逗号分隔的页码范围。";					
				case OZCStringResource.IDS_OPT_CHOOSEPAGE_DETAIL2:
					return "(例如:1.3.5-12)";					
				case OZCStringResource.IDS_OPT_COMPONENT_LABEL:
					return "指定要保存的组件";					
				case OZCStringResource.IDS_OPT_LABEL:
					return "标签";
				case OZCStringResource.IDS_OPT_IMAGE:
					return "图片";
				case OZCStringResource.IDS_OPT_BARCODE:
					return "条形码";					
				case OZCStringResource.IDS_OPT_PDF417:
					return "PDF417 码";					
				case OZCStringResource.IDS_OPT_QR_BARCODE:
					return "QR 码";					
				case OZCStringResource.IDS_OPT_HTML:
					return "HTML";	
				case OZCStringResource.IDS_OPT_CHART:
					return "图表";			
				case OZCStringResource.IDS_OPT_LINE:
					return "直线";			
				case OZCStringResource.IDS_OPT_CIRCLE:
					return "椭圆";			
				case OZCStringResource.IDS_OPT_RECTANGLE:
					return "矩形";			
				case OZCStringResource.IDS_OPT_ARROW:
					return "箭头";				
				case OZCStringResource.IDS_OPT_URL_LINK:
					return "URL链接";			
				case OZCStringResource.IDS_OPT_LIST_LINK:
					return "目录链接";		
				case OZCStringResource.IDS_OPT_PDF_SAVE:
					return "PDF 保存选项";		
				case OZCStringResource.IDS_OPT_DOC_IMFOR:
					return "文档信息";			
				case OZCStringResource.IDS_OPT_OPEN_PROTECT:
					return "限制打开";					
				case OZCStringResource.IDS_OPT_EDIT_PROTECT:
					return "限制修改";
				case OZCStringResource.IDS_OPT_USER_RIGHT:
					return "文档限制";							
				case OZCStringResource.IDS_OPT_TITLE:
					return "标题";
				case OZCStringResource.IDS_OPT_WRITER:
					return "作者";			
				case OZCStringResource.IDS_OPT_SUBJECT:
					return "主题";			
				case OZCStringResource.IDS_OPT_KEYWORD:
					return "关键字";
				case OZCStringResource.IDS_OPT_APPLICATION:
					return "应用程序";			
				case OZCStringResource.IDS_OPT_USER_PW:
					return "打开口令";
				case OZCStringResource.IDS_OPT_USER_PW_CONFIRM:
					return "确认打开口令";						
				case OZCStringResource.IDS_OPT_MASTER_PW:
					return "修改口令";
				case OZCStringResource.IDS_OPT_MASTER_PW_CONFIRM:
					return "确认修改口令";		
				case OZCStringResource.IDS_OPT_PRINT_PERMIT:
					return "允许打印";
				case OZCStringResource.IDS_OPT_CONTENTS_COPY:
					return "允许内容复制";
				case OZCStringResource.IDS_OPT_SAVEIMG_TOCHART:
					return "将图表保存为图片";
				case OZCStringResource.IDS_OPT_FONT_INCLUDE:
					return "폰트 포함";			
				case OZCStringResource.IDS_OPT_PRINT:
					return "打印";
				case OZCStringResource.IDS_OPT_PRINT_COUNT:
					return "打印份数";		
				case OZCStringResource.IDS_OPT_PRINT_SCOPE:
					return "打印范围";
				case OZCStringResource.IDS_OPT_PRINT_RULE:
					return "打印方式";			
				case OZCStringResource.IDS_OPT_COUNT:
					return "份";
				case OZCStringResource.IDS_OPT_PRINT_ONEPAGE:
					return "逐份打印";
				case OZCStringResource.IDS_OPT_PRINT_ALL_REPORT:
					return "打印所有报表";					
				case OZCStringResource.IDS_OPT_ALL:
					return "全部";					
				case OZCStringResource.IDS_OPT_CHOOSE_PAGE:
					return "自定义";	
				case OZCStringResource.IDS_OPT_PRINT_FITTOPAGE:
					return "按纸张大小缩放";		
				case OZCStringResource.IDS_OPT_DEVIDE_PRINT:
					return "分割打印";
				case OZCStringResource.IDS_OPT_DEVIDE_PRINT_TOFITPAPER:
					return "按纸张分割打印";					
				case OZCStringResource.IDS_TOGETHER_PRINT:
					return "多页";					
				case OZCStringResource.IDS_OPT_PAGECOUNT_TOPRINT:
					return "每面打印页数";	
				case OZCStringResource.IDS_OPT_PRINT_ORDER:
					return "页面顺序";										
				case OZCStringResource.IDS_OPT_HORIZON:
					return "水平";										
				case OZCStringResource.IDS_OPT_VERTICAL:
					return "垂直";										
				case OZCStringResource.IDS_OPT_PRINT_DIRECTION:
					return "打印方向";										
				case OZCStringResource.IDS_OPT_PORTRAIT:
					return "垂直";										
				case OZCStringResource.IDS_OPT_LANDSCAPE:
					return "水平";								
				case OZCStringResource.IDS_OPT_OPTION:
					return "选项";		
				case OZCStringResource.IDS_OPT_CONFIRM:
					return "确定";	
				case OZCStringResource.IDS_OPT_CANCEL:
					return "取消";							
				case OZCStringResource.IDS_OPT_SAVE:
					return "저장";					
				case OZCStringResource.IDS_OPT_OZD_SAVE_OPTION:
					return "保存选项";								
				case OZCStringResource.IDS_OPT_PASSWORD:
					return "密码";		
				case OZCStringResource.IDS_OPT_MEMOADD_OK:
					return "包含编辑内容";		
				case OZCStringResource.IDS_OPT_SAVE_ALLREPORT:
					return "保存所有报表";						
				case OZCStringResource.IDS_OPT_INCLUDE_IMG:
					return "包含图片";						
				case OZCStringResource.IDS_OPT_FILE_TYPE:
					return "文件类型";			
				case OZCStringResource.IDS_OPT_EXCEL_SAVE:
					return "Excel 保存选项";
				case OZCStringResource.IDS_OPT_EXCEL_SAVE2:
					return "Excel 保存选项(高级)";					
				case OZCStringResource.IDS_OPT_NEXCEL_SAVE:
					return "Nexcel 保存选项";
				case OZCStringResource.IDS_OPT_WORD_SAVE:
					return "Word 保存选项";						
				case OZCStringResource.IDS_OPT_PPT_SAVE:
					return "PowerPoint 保存选项";		
				case OZCStringResource.IDS_OPT_HTML_SAVE:
					return "HTML 保存选项";		
				case OZCStringResource.IDS_OPT_CSV_SAVE:
					return "CSV 保存选项";		
				case OZCStringResource.IDS_OPT_TEXT_SAVE:
					return "TEXT 保存选项";		
				case OZCStringResource.IDS_OPT_JPG_SAVE:
					return "JPG 保存选项";
				case OZCStringResource.IDS_OPT_TIFF_SAVE:
					return "TIFF 保存选项";		
				case OZCStringResource.IDS_OPT_HWP_SAVE:
					return "Hangul Option";		
				case OZCStringResource.IDS_OPT_HWP97_SAVE:
					return "Hangul97 Option";		
				case OZCStringResource.IDS_OPT_MHT_SAVE:
					return "MHT 保存选项";				
				case OZCStringResource.IDS_OPT_SAVE_FORM:
					return "分隔标记";							
				case OZCStringResource.IDS_OPT_PAGE:
					return "页面";
				case OZCStringResource.IDS_OPT_FONT:
					return "字体";
				case OZCStringResource.IDS_OPT_FORMAT:
					return "格式";
				case OZCStringResource.IDS_OPT_AUTO_FIT:
					return "自动调整";
				case OZCStringResource.IDS_OPT_CELL_MINIMUM_SIZE_CELL:
					return "单元格最小值";
				case OZCStringResource.IDS_OPT_SAVE_MODE:
					return "保存类型";			
				case OZCStringResource.IDS_OPT_KEEP_FORM_ATTR:
					return "保持报表原样，属性";			
				case OZCStringResource.IDS_OPT_IGNORE_COLOR_OUTLINE_CELL:
					return "不含颜色，边框，单元格合并";
				case OZCStringResource.IDS_OPT_REMOVE_CELL_AFTERDIVIDE:
					return "拆分后删除周边单元格";
				case OZCStringResource.IDS_OPT_SAVE_SHEET_PER_COLPAGE:
					return "一列的页保存为一个工作表";
				case OZCStringResource.IDS_OPT_SAVE_SHEET_PAGE:
					return "一页保存为一个工作表";
				case OZCStringResource.IDS_OPT_COLUMN_FIRST:
					return "先行后列";
				case OZCStringResource.IDS_OPT_ROW_FIRST:
					return "先列后行";							
				case OZCStringResource.IDS_OPT_REMOVE_PAGE_GAP:
					return "除去页间空白";
				case OZCStringResource.IDS_OPT_SIZE_COLON:
					return "字号:";
				case OZCStringResource.IDS_OPT_REMOVE_LINE:
					return "删除行";		
				case OZCStringResource.IDS_OPT_EXCLUDE_FIRST_PAGE:
					return "第一页除外";
				case OZCStringResource.IDS_OPT_ONLY_FIRST_PAGE:
					return "只选第一页";
				case OZCStringResource.IDS_OPT_ALL_PAGE:
					return "所有页";						
				case OZCStringResource.IDS_OPT_LINE_SCOPE_LINE_NUMBER:
					return "请键入行号和/或用逗号(, )\n分隔的行范围。例如）1-3，5，12";					
				case OZCStringResource.IDS_OPT_CHANGE_TXT_TO_INTEGER:
					return "把文本型数字转换为数字";
				case OZCStringResource.IDS_OPT_APPLY_ONLYLABEL_NUMBER:
					return "只适用数值型标签";
				case OZCStringResource.IDS_OPT_CHANGE_AS_NORMAL:
					return "转换为常规(单元格格式)";						
				case OZCStringResource.IDS_OPT_FONT_COLON:
					return "字体:";					
				case OZCStringResource.IDS_OPT_WIDTH:
					return "宽度";
				case OZCStringResource.IDS_OPT_HEIGHT:
					return "高度";
				case OZCStringResource.IDS_OPT_SAVE_MHT_TYPE:
					return "以mht格式保存 （支持office XP或更高版本）";					
				case OZCStringResource.IDS_OPT_TABLE_SAVE_WAY:
					return "表";
				case OZCStringResource.IDS_OPT_TABLE_TYPE_WAY:
					return "保存为表格           ";
				case OZCStringResource.IDS_OPT_BAND_SAVE_WAY:
					return "Band";
				case OZCStringResource.IDS_OPT_BAND_SAVE_PAGE:
					return "将页眉/页脚Band保存在页眉/页脚";						
				case OZCStringResource.IDS_OPT_PPT_SAVE_WAY:
					return "保存类型";
				case OZCStringResource.IDS_OPT_MHTD_SAVE_PAGE:
					return "保存为 mht 格式(支持 office XP 以上)";				
				case OZCStringResource.IDS_OPT_FONT_SIZE_SETTING:
					return "设置字号";
				case OZCStringResource.IDS_OPT_PAGE_BETWEEN:
					return "页间间隔";
				case OZCStringResource.IDS_OPT_PER_PAGE_SAVE:
					return "分页保存";								
				case OZCStringResource.IDS_OPT_LINE_COUNT_COLON:
					return "行数 :";	
				case OZCStringResource.IDS_OPT_OFFSET_COLON:
					return "偏移量 :";
				case OZCStringResource.IDS_OPT_STARTPOINT_HTMLDOC:
					return "文档开始位置";					
				case OZCStringResource.IDS_OPT_ENCODING_COLON:
					return "编码 :";					
				case OZCStringResource.IDS_OPT_X_AXIS:
					return "X轴";							
				case OZCStringResource.IDS_OPT_Y_AXIS:
					return "Y轴";	
				case OZCStringResource.IDS_OPT_OFFSET:
					return "偏移";	
				case OZCStringResource.IDS_OPT_SEPARATION:
					return "分隔符";							
				case OZCStringResource.IDS_OPT_SEPARATOR:
					return "分隔符";	
				case OZCStringResource.IDS_OPT_USER_DEFINE:
					return "自定义";						
				case OZCStringResource.IDS_OPT_SEPARATION_TYPE:
					return "分页符";
				case OZCStringResource.IDS_OPT_PAGE_BRACKET:
					return "<PAGE>";
				case OZCStringResource.IDS_OPT_SET_PAGELINE:
					return "空白行数";
				case OZCStringResource.IDS_OPT_CHAR_NUMBER:
					return "数字";					
				case OZCStringResource.IDS_OPT_ADD_SEPARATOR:
					return "添加千位分隔符";					
				case OZCStringResource.IDS_OPT_REMOVE_SEPARATOR:
					return "取消千位分隔符";			
				case OZCStringResource.IDS_OPT_PAGE_SEPARATION:
					return "分页符";						
				case OZCStringResource.IDS_OPT_LABEL_SAVE:
					return "标签保存方式";		
				case OZCStringResource.IDS_OPT_LABEL_SIZE:
					return "固定标签尺寸";
				case OZCStringResource.IDS_OPT_POSITION_SAVE:
					return "位置保存方式";
				case OZCStringResource.IDS_OPT_VERTREL_TOPARA:
					return "将纵向设置为文段";
				case OZCStringResource.IDS_OPT_BACKGROUND_BAND:
					return "Save background band to background page";				
				case OZCStringResource.IDS_OPT_TITLE_COLON:
					return "标题  :";	
				case OZCStringResource.IDS_OPT_DISPLAY_OPTION:
					return "显示选项";	
				case OZCStringResource.IDS_OPT_GRIDLINES:
					return "显示网格线";		
				case OZCStringResource.IDS_OPT_ENCODING_WAY:
					return "编码方式";
				case OZCStringResource.IDS_OPT_ENCODING_G3:
					return "G3";	
				case OZCStringResource.IDS_OPT_ENCODING_G4:
					return "G4";		
				case OZCStringResource.IDS_OPT_ENCODING_SAVEAS_ONEFILE:
					return "单文档保存";
				case OZCStringResource.IDS_OPT_RATIO:
					return "比例";	
				case OZCStringResource.IDS_OPT_ZOOM_INOUT:
					return "缩放比例";
				case OZCStringResource.IDS_OPT_SET_DPI:
					return "根据分辨率调整大小";
				case OZCStringResource.IDS_OPT_DPI:
					return "分辨率";
				case OZCStringResource.IDS_OPT_SIZE:
					return "字号";
				case OZCStringResource.IDS_OPT_SAVE_RATIO:
					return "缩放保存";
				case OZCStringResource.IDS_OPT_COMPRESSION:
					return "压缩";
				case OZCStringResource.IDS_OPT_QUALITY:
					return "质量";				
				case OZCStringResource.IDS_OPT_USE_INDENT:
					return "使用缩进功能";						
				case OZCStringResource.IDS_OPT_DATA_SAVE:
					return "数据保存";	
				case OZCStringResource.IDS_OPT_FIND_WHAT:
					return "查找内容:";	
				case OZCStringResource.IDS_OPT_MATCH_WHOLE_WORD:
					return "全字匹配";
				case OZCStringResource.IDS_OPT_MATCH_CASE:
					return "区分大小写";
				case OZCStringResource.IDS_OPT_DIRECTION:
					return "方向";			
				case OZCStringResource.IDS_OPT_SEARCH_UP:
					return "向上";
				case OZCStringResource.IDS_OPT_SEARCH_DOWN:
					return "向下";
				case OZCStringResource.IDS_OPT_NEXT:
					return "查找下一处";			
				case OZCStringResource.IDS_OPT_REFRESH_REPORT:
					return "刷新";		
				case OZCStringResource.IDS_OPT_INTERVAL:
					return "周期";	
				case OZCStringResource.IDS_OPT_HH:
					return "时";		
				case OZCStringResource.IDS_OPT_MM:
					return "分";		
				case OZCStringResource.IDS_OPT_SS:
					return "秒";	
				case OZCStringResource.IDS_OPT_REFRESH_NOW:
					return "刷新";		
				case OZCStringResource.IDS_OPT_START:
					return "开始";		
				case OZCStringResource.IDS_OPT_STOP:
					return "停止";							
				case OZCStringResource.IDS_OPT_LEFT_COLON:
					return "左:";
				case OZCStringResource.IDS_OPT_RIGHT_COLON:
					return "右:";
				case OZCStringResource.IDS_OPT_TOP_COLON:
					return "上:";
				case OZCStringResource.IDS_OPT_BOTTOM_COLON:
					return "下:";			
				case OZCStringResource.IDS_OPT_PAGE_SETUP:
					return "页面设置";	
				case OZCStringResource.IDS_OPT_MARGIN:
					return "页边距";	
				case OZCStringResource.IDS_OPT_UNIT:
					return "度量单位";		
				case OZCStringResource.IDS_OPT_UNIT_COLON:
					return "单位 :";	
				case OZCStringResource.IDS_OPT_MARGIN_OPTION:
					return "页边距选项";		
				case OZCStringResource.IDS_OPT_MARGIN_DEFAULT:
					return "默认页边距";	
				case OZCStringResource.IDS_OPT_MARGIN_AUTO_ADJUST:
					return "自动调整";				
				case OZCStringResource.IDS_OPT_HTML_VERSION:
					return "版本";		
				case OZCStringResource.IDS_OPT_HTML_VERSION_COLON:
					return "HTML 版本 :";	
				case OZCStringResource.IDS_OPT_TOTAL:
					return "全部";	
				case OZCStringResource.IDS_OPT_REPORT_STRUCTURE:
					return "查看报表结构";		
				case OZCStringResource.IDS_OPT_LABEL_FORMAT:
					return "标签格式";				
				case OZCStringResource.IDS_OPT_PARAGRAPH:
					return "段落";	
				case OZCStringResource.IDS_OPT_FILL:
					return "填充";	
				case OZCStringResource.IDS_OPT_LINECOLOR:
					return "边框";	
				case OZCStringResource.IDS_OPT_ETC:
					return "其它";		
				case OZCStringResource.IDS_OPT_ALIGNMENT:
					return "文本对齐方式";		
				case OZCStringResource.IDS_OPT_TXT_DIRECTION:
					return "文字方向";	
				case OZCStringResource.IDS_OPT_INTERNAL_MARGIN:
					return "边距";	
				case OZCStringResource.IDS_OPT_PREVIEW:
					return "预览";	
				case OZCStringResource.IDS_OPT_HORIZON_COLON:
					return "水平 :";										
				case OZCStringResource.IDS_OPT_VERTICAL_COLON:
					return "垂直 :";	
				case OZCStringResource.IDS_OPT_LINE_SPACE_COLON:
					return "行距 :";										
				case OZCStringResource.IDS_OPT_INDENT_COLON:
					return "缩进 :";		
				case OZCStringResource.ID_OPT_PARAMETERS_TITLE_LABEL:
					return "更改参数值";
				case OZCStringResource.IDS_OPT_EXIT:
					return "结束";						
				case OZCStringResource.IDS_OPT_OZZPASSWORD:
					return "密码";		
				case OZCStringResource.IDS_OPT_OZZPASSWORD_CHECK:
					return "确认密码";		
				case OZCStringResource.IDS_OPT_ID:
					return "用户名";		
				case OZCStringResource.IDS_OPT_SECURITY:
					return "安全";		
				case OZCStringResource.IDS_OPT_PRODUCT_INFO:
					return "产品信息";		
				case OZCStringResource.IDS_OPT_OZ_INFO:
					return "产品 信息";			
				case OZCStringResource.IDS_OPT_ERROR_CODE_COLON:
					return "错误代码:";			
				case OZCStringResource.IDS_OPT_GENERAL_MSG_COLON:
					return "信息:";		
				case OZCStringResource.IDS_OPT_VIEW_DETAIL:
					return "详细";		
				case OZCStringResource.IDS_OPT_DETAIL_MSG_COLON:
					return "详细信息:";	
				case OZCStringResource.IDS_OPT_TEXT_CONTROL:
					return "文本控制";							
				case OZCStringResource.IDS_OPT_SHRINK_TO_FIT:
					return "缩小字体填充";		
				case OZCStringResource.IDS_OPT_PASSWORD2:
					return "确定";		
				case OZCStringResource.IDS_OPT_DM_BARCODE:
					return "DataMatrix 码";			
				case OZCStringResource.IDS_OPT_FONT_STYLE_COLON:
					return "字形:";
				case OZCStringResource.IDS_OPT_COLOR_COLON:
					return "颜色:";
				case OZCStringResource.IDS_OPT_INTERVAL_COLON:
					return "字距:";					
				case OZCStringResource.IDS_OPT_STRETCH_COLON:
					return "Stretch :";					
				case OZCStringResource.IDS_OPT_EFFECT:
					return "效果";	
				case OZCStringResource.IDS_OPT_UNDERLINE:
					return "下划线";					
				case OZCStringResource.IDS_OPT_STRIKEOUT:
					return "删除线";					
				case OZCStringResource.IDS_OPT_DOUBLE_STRIKEOUT:
					return "双删除线";	
				case OZCStringResource.IDS_OPT_NO_FILL:
					return "无填充";
				case OZCStringResource.IDS_OPT_TRANSPARENCY:
					return "透明度:";
				case OZCStringResource.IDS_OPT_GRADIENT:
					return "渐变";	
				case OZCStringResource.IDS_OPT_USE_GRADIENT:
					return "渐变填充";	
				case OZCStringResource.IDS_OPT_SLASH:
					return "斜上";
				case OZCStringResource.IDS_OPT_BACK_SLASH:
					return "斜下";	
				case OZCStringResource.IDS_OPT_START_POSITION:
					return "起点";	
				case OZCStringResource.IDS_OPT_FROM_EDGE:
					return "角部";	
				case OZCStringResource.IDS_OPT_DIRECT:
					return "正向";	
				case OZCStringResource.IDS_OPT_FROM_CENTER:
					return "中心";	
				case OZCStringResource.IDS_OPT_REVERSE:
					return "反向";					
				case OZCStringResource.IDS_OPT_THICKNESS_W:
					return "粗细(W):";	
				case OZCStringResource.IDS_OPT_STYLE_Y:
					return "线型(Y):";	
				case OZCStringResource.IDS_OPT_COLOR_C:
					return "颜色(C):";		
				case OZCStringResource.IDS_OPT_CLIPPING:
					return "裁剪";	
				case OZCStringResource.IDS_OPT_WORD_WRAP:
					return "自动换行";	
				case OZCStringResource.IDS_OPT_WORD_WRAP_TYPE:
					return "自动换行方式";	
				case OZCStringResource.IDS_OPT_IGNORE_SPACE:
					return "忽略空格";	
				case OZCStringResource.IDS_OPT_NON_ASCII_WORD:
					return "防止切断单词";	
				case OZCStringResource.IDS_OPT_TOOLTIP_TEXT:
					return "工具提示...";	
				case OZCStringResource.IDS_OPT_BASIC:
					return "Basic";
				case OZCStringResource.IDS_OPT_OUTLINE:
					return "Outline";
				case OZCStringResource.IDS_OPT_3D:
					return "3D";
				case OZCStringResource.IDS_OPT_ENGRAVE:
					return "Engrave";
				case OZCStringResource.IDS_OPT_SEGMENT:
					return "Segment";
				case OZCStringResource.IDS_OPT_HOLLOW:
					return "Hollow";
				case OZCStringResource.IDS_OPT_SHADOW:
					return "Shadow";						
				case OZCStringResource.IDS_OPT_IMAGE_STYLE:
					return "样式 :";		
				case OZCStringResource.IDS_OPT_BARCODE_STYLE:
					return "类型:";
				case OZCStringResource.IDS_OPT_DATA_GAP:
					return "数据间距:";
				case OZCStringResource.IDS_OPT_DATA_POSITION:
					return "数据位置:";
				case OZCStringResource.IDS_OPT_BARCODE_MARGIN:
					return "边距:";	
				case OZCStringResource.IDS_OPT_ALIGN_HORIZONTALLY:
					return "水平对齐:";
				case OZCStringResource.IDS_OPT_RATION_COLON:
					return "比例:";
				case OZCStringResource.IDS_OPT_PRINTING_SCALE:
					return "打印比例:";
				case OZCStringResource.IDS_OPT_ROTATION_COLON:
					return "旋转:";		
				case OZCStringResource.IDS_OPT_TITLE_GAP:
					return "标题间距:";
				case OZCStringResource.IDS_OPT_TITLE_POSITION:
					return "标题位置:";
				case OZCStringResource.IDS_OPT_BARCODE_ERROR_OPTION:
					return "显示错误:";		
				case OZCStringResource.IDS_OPT_DISPLAY_ERROR_DATA:
					return "数据错误";		
				case OZCStringResource.IDS_OPT_DISPLAY_ERROR_SIZE:
					return "尺寸错误";		
				case OZCStringResource.IDS_OPT_DISPLAY_EMPTY_DATA:
					return "空数据";
				case OZCStringResource.IDS_OPT_PDF_IMAGE_STYLE:
					return "图像样式:";		
				case OZCStringResource.IDS_OPT_PDF_ROW_NUMBER:
					return "行数:";		
				case OZCStringResource.IDS_OPT_PDF_COL_NUMBER:
					return "列数:";		
				case OZCStringResource.IDS_OPT_PDF_ECC:
					return "纠错等级:";							
				case OZCStringResource.IDS_OPT_PDF_X_SCALE:
					return "X 大小:";		
				case OZCStringResource.IDS_OPT_PDF_Y_SCALE:
					return "Y 大小:";	
				case OZCStringResource.IDS_OPT_BORDER_COLOR:
					return "边框颜色 :";	
				case OZCStringResource.IDS_OPT_FILL_COLOR:
					return "填充颜色 :";	
				case OZCStringResource.IDS_OPT_THICKNESS:
					return "粗细 :";	
				case OZCStringResource.IDS_OPT_SHAPE_NO_FILL:
					return "透明 :";	
				case OZCStringResource.IDS_OPT_LINE_TYPE:
					return "类型 :";	
				case OZCStringResource.IDS_OPT_TOOLTIP_TEXT_COLON:
					return "工具提示 :";	
				case OZCStringResource.IDS_OPT_EDIT_CHART:
					return "编辑图表";	
				case OZCStringResource.IDS_OPT_SHOW_DATA:
					return "显示数据标签";		
				case OZCStringResource.IDS_OPT_EDIT_SHAPE:
					return "编辑形状";		
				case OZCStringResource.IDS_FORCS_RV:
					return "Forcs(R) OZ Report Viewer";							
				case OZCStringResource.IDS_OPT_EXCEL_LINE:
					return "线条";							
				case OZCStringResource.IDS_OPT_EXCEL_SAVE_TYPE:
					return "保存形式";						
				case OZCStringResource.IDS_OPT_JPG_ZOOM_INOUT:
					return "缩放比例";					
				case OZCStringResource.IDS_OPT_ZOOM_INOUT_COLON:
					return "缩放比例: ";
				case OZCStringResource.IDS_OPT_QUALITY_COLON:
					return "质量: ";
				case OZCStringResource.IDS_OPT_TIFF_SIZE:
					return "自定义大小";	
				case OZCStringResource.IDS_OPT_PAGELINE:
					return "Line";
				case OZCStringResource.IDS_OPT_WORD_BAND_SAVE_PAGE:
					return "将 Page Header/Footer Band 保存到页眉/页脚";	
				case OZCStringResource.IDS_OPT_SAVE_OPTION:
					return "保存方式";
				case OZCStringResource.IDS_OPT_PRINT_MULTIDOC:
					return "多文档";	
				case OZCStringResource.ID_PAGE_CONTROL_WINDOW:
					return "页面导航";		
				case OZCStringResource.IDS_OPT_FONT_NAME_COLON:
					return "字体:";
				case OZCStringResource.IDS_OPT_FONT_OPTION:
					return "选项";
				case OZCStringResource.IDS_OPT_AUTO_SELECTED:
					return "自动调整";		
				case OZCStringResource.IDS_OPT_SIDE:
					return "宽度";
				case OZCStringResource.IDS_OPT_LENGTH:
					return "高度";		
				case OZCStringResource.IDS_OPT_OTHER:
					return "自定义";	
				case OZCStringResource.IDS_OPT_FONT_SAVE_WAY:
					return "字体";	
				case OZCStringResource.IDS_OPT_HORIZONTAL_NAVI:
					return "水平";										
				case OZCStringResource.IDS_OPT_VERTICAL_NAVI:
					return "垂直";	
				case OZCStringResource.IDS_OPT_PNG_SAVE:
					return "PNG 保存选项";
				case OZCStringResource.IDS_OPT_GIF_SAVE:
					return "GIF 保存选项";	
				case OZCStringResource.IDS_OPT_REMOVE_LINES:
					return "删除行";						
				case OZCStringResource.IDS_OPT_ENCODING_HDM:
					return "编码 : ";			
				case OZCStringResource.IDS_OPT_SVG_SAVE:
					return "SVG 保存选项";						
				case OZCStringResource.IDS_OPT_PDF_CHANGEABLE:
					return "允许更改";	
				case OZCStringResource.IDS_OPT_PDF_RADIO_CHANGEABLE1:
					return "无";	
				case OZCStringResource.IDS_OPT_PDF_RADIO_CHANGEABLE2:
					return "注释, 填写表单和签名现有签名域";	
				case OZCStringResource.IDS_OPT_PDF_RADIO_CHANGEABLE3:
					return "页面布局, 填写表单和签名现有签名域";	
				case OZCStringResource.IDS_OPT_PDF_RADIO_CHANGEABLE4:
					return "除了提取页面";	
				case OZCStringResource.IDS_OPT_SAVE_WAY_BASE:
					return "保存方式";		
				case OZCStringResource.IDS_OPT_SAVE_LARGEBUNDLE_BYPAGE:
					return "单页/分页绑定报表";						
				case OZCStringResource.IDS_OPT_SAVE_LARGE_BUNDLE:
					return "单页保存";		
				case OZCStringResource.IDS_OPT_SAVE_BY_PAGE:
					return "分页保存";	
				case OZCStringResource.IDS_OPT_KEEP_SAVING:
					return "keep saving";					
				case OZCStringResource.IDC_TITLE_SECURITY:
					return "限制打开";		
				case OZCStringResource.IDC_EDIT_PASSWORD:
					return "打开口令";	
				case OZCStringResource.IDC_EDIT_PASSWORD_VERIFY:
					return "确认打开口令";	
				case OZCStringResource.IDC_ZIP_GROUP:
					return "压缩";	
				case OZCStringResource.IDC_COMPRESS_ON_SAVE:
					return "压缩保存";	
				case OZCStringResource.IDS_OPT_MARGIN_COLON:
					return "边距 :";						
				case OZCStringResource.IDC_PAPER_SETUP:
					return "纸张设置";					
				case OZCStringResource.IDC_CHECK_ADJUST:
					return "按纸张大小缩放";						
					
				case OZCStringResource.IDS_CHECK_EXCEL_ZOOM:
					return "比例";
				case OZCStringResource.IDS_CHECK_EXCEL_ZOOM_RATIO2:
					return "缩放比例(%)";					
				case OZCStringResource.IDS_CHECK_EXCEL_ZOOM_RATIO:
					return "缩放比例: ";							
				case OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES:
					return "凍結窗格";							
				case OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES_ROW:
					return "行: ";							
				case OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES_COL:
					return "列: ";							
				case OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES_ROW_M:
					return "行 ";							
				case OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES_COL_M:
					return "列 ";							
			}
		}
		if(CLocale.GetBaseLang_Flash().localeCompare("zh-TW") == 0) {
			switch (id) {
				case OZCStringResource.IDS_OPT_SAVEAS_DEFAULT:
					return "基本選項";
				case OZCStringResource.IDS_OPT_SAVEAS:
					return "保存選項";
				case OZCStringResource.IDS_OPT_SAVE_WAY_TXT:
					return "Save report after binding per one page/pages.";
				case OZCStringResource.IDS_OPT_SAVE_WAY_ONEPAGE:
					return "Save Per OnePage";
				case OZCStringResource.IDS_OPT_SAVE_WAY_EACHPAGE:
					return "Save Per Pages";	

				case OZCStringResource.IDS_OPT_SAVE_WAY:
					return "保存方式";
				case OZCStringResource.IDS_OPT_SAVE_ACTION:
					return "Action";
				case OZCStringResource.IDS_OPT_SAVE_MULTIDOC:
					return "多文檔";
				case OZCStringResource.IDS_OPT_SAVE_AREA:
					return "保存領域";
				case OZCStringResource.IDS_OPT_SAVE_COMPONENT:
					return "組件";
				case OZCStringResource.IDS_OPT_SAVE_LINK_INFO:
					return "保存鏈結";	
				case OZCStringResource.IDS_OPT_ACTION_AFTER_SAVE:
					return "Action after save";					
				case OZCStringResource.IDS_OPT_SAVE_ONEFILE:
					return "保存為一個檔";					
				case OZCStringResource.IDS_OPT_SAVE_ALLFILE:
					return "保存所有文檔";		
				case OZCStringResource.IDS_OPT_ALLPAGE:
					return "全部頁";					
				case OZCStringResource.IDS_OPT_CURRENTPAGE:
					return "當前頁";					
				case OZCStringResource.IDS_OPT_SELECTEDPAGE:
					return "被選頁";		
				case OZCStringResource.IDS_OPT_CHOOSEPAGE:
					return "頁碼範圍 : ";		
				case OZCStringResource.IDS_OPT_CHOOSEPAGE_DETAIL:
					return "請鍵入頁碼和/或用逗號分隔的頁碼範圍。";					
				case OZCStringResource.IDS_OPT_CHOOSEPAGE_DETAIL2:
					return "(例如：1.3.5-12)";					
				case OZCStringResource.IDS_OPT_COMPONENT_LABEL:
					return "設定要保存的組件";					
				case OZCStringResource.IDS_OPT_LABEL:
					return "標籤";
				case OZCStringResource.IDS_OPT_IMAGE:
					return "圖片";
				case OZCStringResource.IDS_OPT_BARCODE:
					return "條碼";					
				case OZCStringResource.IDS_OPT_PDF417:
					return "PDF417 條碼";					
				case OZCStringResource.IDS_OPT_QR_BARCODE:
					return "QR 條碼";					
				case OZCStringResource.IDS_OPT_HTML:
					return "HTML";	
				case OZCStringResource.IDS_OPT_CHART:
					return "圖表";			
				case OZCStringResource.IDS_OPT_LINE:
					return "線條";			
				case OZCStringResource.IDS_OPT_CIRCLE:
					return "圓形";			
				case OZCStringResource.IDS_OPT_RECTANGLE:
					return "矩形";			
				case OZCStringResource.IDS_OPT_ARROW:
					return "箭頭";					
				case OZCStringResource.IDS_OPT_URL_LINK:
					return "URL鏈結";			
				case OZCStringResource.IDS_OPT_LIST_LINK:
					return "目錄鏈結";			
				case OZCStringResource.IDS_OPT_PDF_SAVE:
					return "PDF 保存選項";		
				case OZCStringResource.IDS_OPT_DOC_IMFOR:
					return "文檔信息";			
				case OZCStringResource.IDS_OPT_OPEN_PROTECT:
					return "限制打開";					
				case OZCStringResource.IDS_OPT_EDIT_PROTECT:
					return "限制修改";
				case OZCStringResource.IDS_OPT_USER_RIGHT:
					return "用戶訪問許可權";							
				case OZCStringResource.IDS_OPT_TITLE:
					return "標題";
				case OZCStringResource.IDS_OPT_WRITER:
					return "作者";			
				case OZCStringResource.IDS_OPT_SUBJECT:
					return "主題";			
				case OZCStringResource.IDS_OPT_KEYWORD:
					return "關鍵字";
				case OZCStringResource.IDS_OPT_APPLICATION:
					return "應用程式";			
				case OZCStringResource.IDS_OPT_USER_PW:
					return "打開口令";
				case OZCStringResource.IDS_OPT_USER_PW_CONFIRM:
					return "確認打開口令";						
				case OZCStringResource.IDS_OPT_MASTER_PW:
					return "修改口令";
				case OZCStringResource.IDS_OPT_MASTER_PW_CONFIRM:
					return "確認修改口令";		
				case OZCStringResource.IDS_OPT_PRINT_PERMIT:
					return "允許列印";
				case OZCStringResource.IDS_OPT_CONTENTS_COPY:
					return "複製內容";
				case OZCStringResource.IDS_OPT_SAVEIMG_TOCHART:
					return "圖表保存為圖片";
				case OZCStringResource.IDS_OPT_FONT_INCLUDE:
					return "폰트 포함";			
				case OZCStringResource.IDS_OPT_PRINT:
					return "列印";
				case OZCStringResource.IDS_OPT_PRINT_COUNT:
					return "列印份數";		
				case OZCStringResource.IDS_OPT_PRINT_SCOPE:
					return "列印範圍";
				case OZCStringResource.IDS_OPT_PRINT_RULE:
					return "列印方式";		
				case OZCStringResource.IDS_OPT_COUNT:
					return "份";
				case OZCStringResource.IDS_OPT_PRINT_ONEPAGE:
					return "逐份列印";
				case OZCStringResource.IDS_OPT_PRINT_ALL_REPORT:
					return "列印所有報表";					
				case OZCStringResource.IDS_OPT_ALL:
					return "全部";					
				case OZCStringResource.IDS_OPT_CHOOSE_PAGE:
					return "頁碼範圍";	
				case OZCStringResource.IDS_OPT_PRINT_FITTOPAGE:
					return "按紙張大小縮放";			
				case OZCStringResource.IDS_OPT_DEVIDE_PRINT:
					return "分開列印";
				case OZCStringResource.IDS_OPT_DEVIDE_PRINT_TOFITPAPER:
					return "按紙張分開列印";					
				case OZCStringResource.IDS_TOGETHER_PRINT:
					return "多頁列印";					
				case OZCStringResource.IDS_OPT_PAGECOUNT_TOPRINT:
					return "一張紙上列印的頁數";	
				case OZCStringResource.IDS_OPT_PRINT_ORDER:
					return "列印順序";										
				case OZCStringResource.IDS_OPT_HORIZON:
					return "水平";										
				case OZCStringResource.IDS_OPT_VERTICAL:
					return "垂直";										
				case OZCStringResource.IDS_OPT_PRINT_DIRECTION:
					return "列印方向";										
				case OZCStringResource.IDS_OPT_PORTRAIT:
					return "縱向";										
				case OZCStringResource.IDS_OPT_LANDSCAPE:
					return "橫向";								
				case OZCStringResource.IDS_OPT_OPTION:
					return "選項";		
				case OZCStringResource.IDS_OPT_CONFIRM:
					return "確認";		
				case OZCStringResource.IDS_OPT_CANCEL:
					return "取消";						
				case OZCStringResource.IDS_OPT_SAVE:
					return "저장";		
				case OZCStringResource.IDS_OPT_OZD_SAVE_OPTION:
					return "保存選項";								
				case OZCStringResource.IDS_OPT_PASSWORD:
					return "密碼";		
				case OZCStringResource.IDS_OPT_MEMOADD_OK:
					return "包含編輯內容";		
				case OZCStringResource.IDS_OPT_SAVE_ALLREPORT:
					return "保存所有報表";						
				case OZCStringResource.IDS_OPT_INCLUDE_IMG:
					return "嵌入圖片";						
				case OZCStringResource.IDS_OPT_FILE_TYPE:
					return "文件類型";			
				case OZCStringResource.IDS_OPT_EXCEL_SAVE:
					return "Excel 保存選項";
				case OZCStringResource.IDS_OPT_EXCEL_SAVE2:
					return "Excel 保存選項(Advanced)";					
				case OZCStringResource.IDS_OPT_NEXCEL_SAVE:
					return "Nexcel 保存選項";
				case OZCStringResource.IDS_OPT_WORD_SAVE:
					return "Word 保存選項";						
				case OZCStringResource.IDS_OPT_PPT_SAVE:
					return "PowerPoint 保存選項";		
				case OZCStringResource.IDS_OPT_HTML_SAVE:
					return "HTML 保存選項";		
				case OZCStringResource.IDS_OPT_CSV_SAVE:
					return "CSV 保存選項";		
				case OZCStringResource.IDS_OPT_TEXT_SAVE:
					return "TEXT 保存選項";		
				case OZCStringResource.IDS_OPT_JPG_SAVE:
					return "JPG 保存選項";
				case OZCStringResource.IDS_OPT_TIFF_SAVE:
					return "TIFF 保存選項";		
				case OZCStringResource.IDS_OPT_HWP_SAVE:
					return "Hangul Option";		
				case OZCStringResource.IDS_OPT_HWP97_SAVE:
					return "Hangul97 Option";		
				case OZCStringResource.IDS_OPT_MHT_SAVE:
					return "MHT 保存選項";	
				case OZCStringResource.IDS_OPT_SAVE_FORM:
					return "分隔標記";							
				case OZCStringResource.IDS_OPT_PAGE:
					return "頁";
				case OZCStringResource.IDS_OPT_FONT:
					return "字體";
				case OZCStringResource.IDS_OPT_FORMAT:
					return "格式";
				case OZCStringResource.IDS_OPT_AUTO_FIT:
					return "自動調整";
				case OZCStringResource.IDS_OPT_CELL_MINIMUM_SIZE_CELL:
					return "單元格最小值";
				case OZCStringResource.IDS_OPT_SAVE_MODE:
					return "保存類型";			
				case OZCStringResource.IDS_OPT_KEEP_FORM_ATTR:
					return "保持報表原樣，屬性";			
				case OZCStringResource.IDS_OPT_IGNORE_COLOR_OUTLINE_CELL:
					return "不含顏色，邊框，單元格合併";
				case OZCStringResource.IDS_OPT_REMOVE_CELL_AFTERDIVIDE:
					return "拆分後刪除周邊單元格";
				case OZCStringResource.IDS_OPT_SAVE_SHEET_PER_COLPAGE:
					return "一列的頁保存為一個工作表";
				case OZCStringResource.IDS_OPT_SAVE_SHEET_PAGE:
					return "一頁保存為一個工作表";
				case OZCStringResource.IDS_OPT_COLUMN_FIRST:
					return "先行後列";
				case OZCStringResource.IDS_OPT_ROW_FIRST:
					return "先列後行";		
				case OZCStringResource.IDS_OPT_REMOVE_PAGE_GAP:
					return "除去頁間空白";
				case OZCStringResource.IDS_OPT_SIZE_COLON:
					return "大小:";
				case OZCStringResource.IDS_OPT_REMOVE_LINE:
					return "刪除行";		
				case OZCStringResource.IDS_OPT_EXCLUDE_FIRST_PAGE:
					return "第一頁除外";
				case OZCStringResource.IDS_OPT_ONLY_FIRST_PAGE:
					return "只選第一頁";
				case OZCStringResource.IDS_OPT_ALL_PAGE:
					return "所有頁";						
				case OZCStringResource.IDS_OPT_LINE_SCOPE_LINE_NUMBER:
					return "請鍵入行號和/或用逗號(, )\n分隔的行範圍。例如）1-3，5，12";					
				case OZCStringResource.IDS_OPT_CHANGE_TXT_TO_INTEGER:
					return "把文本型數位轉換為數值";
				case OZCStringResource.IDS_OPT_APPLY_ONLYLABEL_NUMBER:
					return "只適用數值型標籤";
				case OZCStringResource.IDS_OPT_CHANGE_AS_NORMAL:
					return "轉換為常規(單元格格式)";					
				case OZCStringResource.IDS_OPT_FONT_COLON:
					return "字體:";					
				case OZCStringResource.IDS_OPT_WIDTH:
					return "寬度";
				case OZCStringResource.IDS_OPT_HEIGHT:
					return "高度";
				case OZCStringResource.IDS_OPT_SAVE_MHT_TYPE:
					return "以mht格式保存 （支援office XP或更高版本）";					
				case OZCStringResource.IDS_OPT_TABLE_SAVE_WAY:
					return "表格保存方式";
				case OZCStringResource.IDS_OPT_TABLE_TYPE_WAY:
					return "保存為表格           ";
				case OZCStringResource.IDS_OPT_BAND_SAVE_WAY:
					return "Band保存方式";
				case OZCStringResource.IDS_OPT_BAND_SAVE_PAGE:
					return "將頁眉/頁腳Band保存在頁眉/頁腳";							
				case OZCStringResource.IDS_OPT_PPT_SAVE_WAY:
					return "PowerPoint 保存方式";
				case OZCStringResource.IDS_OPT_MHTD_SAVE_PAGE:
					return "以mht格式保存（支援office XP或更高版本）";				
				case OZCStringResource.IDS_OPT_FONT_SIZE_SETTING:
					return "設定字體大小";
				case OZCStringResource.IDS_OPT_PAGE_BETWEEN:
					return "頁間距";
				case OZCStringResource.IDS_OPT_PER_PAGE_SAVE:
					return "分頁保存";								
				case OZCStringResource.IDS_OPT_LINE_COUNT_COLON:
					return "行數 :";	
				case OZCStringResource.IDS_OPT_OFFSET_COLON:
					return "偏移 :";
				case OZCStringResource.IDS_OPT_STARTPOINT_HTMLDOC:
					return "指定HTML文檔的初始坐標";					
				case OZCStringResource.IDS_OPT_ENCODING_COLON:
					return "編碼 : ";					
				case OZCStringResource.IDS_OPT_X_AXIS:
					return "X軸";							
				case OZCStringResource.IDS_OPT_Y_AXIS:
					return "Y軸";				
				case OZCStringResource.IDS_OPT_OFFSET:
					return "偏移";			
				case OZCStringResource.IDS_OPT_SEPARATION:
					return "分隔";							
				case OZCStringResource.IDS_OPT_SEPARATOR:
					return "分隔符號";	
				case OZCStringResource.IDS_OPT_USER_DEFINE:
					return "自定義";						
				case OZCStringResource.IDS_OPT_SEPARATION_TYPE:
					return "分隔標記";
				case OZCStringResource.IDS_OPT_PAGE_BRACKET:
					return "<PAGE>";
				case OZCStringResource.IDS_OPT_SET_PAGELINE:
					return "設定空白行數";
				case OZCStringResource.IDS_OPT_CHAR_NUMBER:
					return "數位文字";					
				case OZCStringResource.IDS_OPT_ADD_SEPARATOR:
					return "添加千位分隔符號";					
				case OZCStringResource.IDS_OPT_REMOVE_SEPARATOR:
					return "去除千位分隔符號";			
				case OZCStringResource.IDS_OPT_PAGE_SEPARATION:
					return "分页";
				case OZCStringResource.IDS_OPT_LABEL_SAVE:
					return "標籤保存方式";		
				case OZCStringResource.IDS_OPT_LABEL_SIZE:
					return "固定標籤尺寸";
				case OZCStringResource.IDS_OPT_POSITION_SAVE:
					return "位置保存方式";
				case OZCStringResource.IDS_OPT_VERTREL_TOPARA:
					return "將縱向設定為文段";
				case OZCStringResource.IDS_OPT_BACKGROUND_BAND:
					return "Save background band to background page";			
				case OZCStringResource.IDS_OPT_TITLE_COLON:
					return "標題 :";		
				case OZCStringResource.IDS_OPT_DISPLAY_OPTION:
					return "顯示選項";	
				case OZCStringResource.IDS_OPT_GRIDLINES:
					return "顯示格線";	
				case OZCStringResource.IDS_OPT_ENCODING_WAY:
					return "編碼方式";
				case OZCStringResource.IDS_OPT_ENCODING_G3:
					return "G3";	
				case OZCStringResource.IDS_OPT_ENCODING_G4:
					return "G4";		
				case OZCStringResource.IDS_OPT_ENCODING_SAVEAS_ONEFILE:
					return "保存在一個檔";
				case OZCStringResource.IDS_OPT_RATIO:
					return "比例";	
				case OZCStringResource.IDS_OPT_ZOOM_INOUT:
					return "放大/縮小";
				case OZCStringResource.IDS_OPT_SET_DPI:
					return "適合解析度的尺寸";
				case OZCStringResource.IDS_OPT_DPI:
					return "解析度";		
				case OZCStringResource.IDS_OPT_SIZE:
					return "大小";	
				case OZCStringResource.IDS_OPT_SAVE_RATIO:
					return "保存縮放";
				case OZCStringResource.IDS_OPT_COMPRESSION:
					return "壓縮";
				case OZCStringResource.IDS_OPT_QUALITY:
					return "品質";			
				case OZCStringResource.IDS_OPT_USE_INDENT:
					return "使用縮進功能";		
				case OZCStringResource.IDS_OPT_DATA_SAVE:
					return "資料保存";	
				case OZCStringResource.IDS_OPT_FIND_WHAT:
					return "尋找目標:";	
				case OZCStringResource.IDS_OPT_MATCH_WHOLE_WORD:
					return "全字須相符";
				case OZCStringResource.IDS_OPT_MATCH_CASE:
					return "大小寫須相符";
				case OZCStringResource.IDS_OPT_DIRECTION:
					return "方向";			
				case OZCStringResource.IDS_OPT_SEARCH_UP:
					return "向上";
				case OZCStringResource.IDS_OPT_SEARCH_DOWN:
					return "向下";
				case OZCStringResource.IDS_OPT_NEXT:
					return "找下一個";				
				case OZCStringResource.IDS_OPT_REFRESH_REPORT:
					return "報表刷新";		
				case OZCStringResource.IDS_OPT_INTERVAL:
					return "週期";	
				case OZCStringResource.IDS_OPT_HH:
					return "時";		
				case OZCStringResource.IDS_OPT_MM:
					return "分";		
				case OZCStringResource.IDS_OPT_SS:
					return "秒";	
				case OZCStringResource.IDS_OPT_REFRESH_NOW:
					return "刷新";		
				case OZCStringResource.IDS_OPT_START:
					return "開始";		
				case OZCStringResource.IDS_OPT_STOP:
					return "停止";	
				case OZCStringResource.IDS_OPT_LEFT_COLON:
					return "左:";
				case OZCStringResource.IDS_OPT_RIGHT_COLON:
					return "右:";
				case OZCStringResource.IDS_OPT_TOP_COLON:
					return "上:";
				case OZCStringResource.IDS_OPT_BOTTOM_COLON:
					return "下:";	
				case OZCStringResource.IDS_OPT_PAGE_SETUP:
					return "頁面設定";	
				case OZCStringResource.IDS_OPT_MARGIN:
					return "頁邊距";	
				case OZCStringResource.IDS_OPT_UNIT:
					return "使用單位系";		
				case OZCStringResource.IDS_OPT_UNIT_COLON:
					return "單位 :";	
				case OZCStringResource.IDS_OPT_MARGIN_OPTION:
					return "頁邊距選項";		
				case OZCStringResource.IDS_OPT_MARGIN_DEFAULT:
					return "使用預設值";	
				case OZCStringResource.IDS_OPT_MARGIN_AUTO_ADJUST:
					return "自動調整";							
				case OZCStringResource.IDS_OPT_HTML_VERSION:
					return "版本";		
				case OZCStringResource.IDS_OPT_HTML_VERSION_COLON:
					return "HTML 版本 :";		
				case OZCStringResource.IDS_OPT_TOTAL:
					return "全部";	
				case OZCStringResource.IDS_OPT_REPORT_STRUCTURE:
					return "查看報表結構";	
				case OZCStringResource.IDS_OPT_LABEL_FORMAT:
					return "標籤格式";
				case OZCStringResource.IDS_OPT_PARAGRAPH:
					return "段落";						
				case OZCStringResource.IDS_OPT_FILL:
					return "填充";							
				case OZCStringResource.IDS_OPT_LINECOLOR:
					return "邊框";						
				case OZCStringResource.IDS_OPT_ETC:
					return "其他";							
				case OZCStringResource.IDS_OPT_ALIGNMENT:
					return "對齊";	
				case OZCStringResource.IDS_OPT_TXT_DIRECTION:
					return "文字方向";						
				case OZCStringResource.IDS_OPT_INTERNAL_MARGIN:
					return "邊距";	
				case OZCStringResource.IDS_OPT_PREVIEW:
					return "預覽";	
				case OZCStringResource.IDS_OPT_HORIZON_COLON:
					return "水平 :";										
				case OZCStringResource.IDS_OPT_VERTICAL_COLON:
					return "垂直 :";	
				case OZCStringResource.IDS_OPT_LINE_SPACE_COLON:
					return "行距 :";										
				case OZCStringResource.IDS_OPT_INDENT_COLON:
					return "縮進 :";							
				case OZCStringResource.ID_OPT_PARAMETERS_TITLE_LABEL:
					return "更改參數值";
				case OZCStringResource.IDS_OPT_EXIT:
					return "結束";														
				case OZCStringResource.IDS_OPT_OZZPASSWORD:
					return "密碼";		
				case OZCStringResource.IDS_OPT_OZZPASSWORD_CHECK:
					return "確認密碼";	
				case OZCStringResource.IDS_OPT_ID:
					return "用戶名";		
				case OZCStringResource.IDS_OPT_SECURITY:
					return "安全";			
				case OZCStringResource.IDS_OPT_PRODUCT_INFO:
					return "產品信息";	
				case OZCStringResource.IDS_OPT_OZ_INFO:
					return "產品 信息";	
				case OZCStringResource.IDS_OPT_ERROR_CODE_COLON:
					return "錯誤代碼:";			
				case OZCStringResource.IDS_OPT_GENERAL_MSG_COLON:
					return "訊息:";		
				case OZCStringResource.IDS_OPT_VIEW_DETAIL:
					return "詳細查看";		
				case OZCStringResource.IDS_OPT_DETAIL_MSG_COLON:
					return "詳細訊息:";	
				case OZCStringResource.IDS_OPT_TEXT_CONTROL:
					return "文字控制";							
				case OZCStringResource.IDS_OPT_SHRINK_TO_FIT:
					return "縮小字型以適合欄寬";				
				case OZCStringResource.IDS_OPT_PASSWORD2:
					return "確認口令";
				case OZCStringResource.IDS_OPT_DM_BARCODE:
					return "DataMatrix 條碼";			
				case OZCStringResource.IDS_OPT_FONT_STYLE_COLON:
					return "字形:";
				case OZCStringResource.IDS_OPT_COLOR_COLON:
					return "顏色 :";
				case OZCStringResource.IDS_OPT_INTERVAL_COLON:
					return "字間距  :";					
				case OZCStringResource.IDS_OPT_STRETCH_COLON:
					return "Stretch :";					
				case OZCStringResource.IDS_OPT_EFFECT:
					return "效果";	
				case OZCStringResource.IDS_OPT_UNDERLINE:
					return "下劃線";					
				case OZCStringResource.IDS_OPT_STRIKEOUT:
					return "刪除線";					
				case OZCStringResource.IDS_OPT_DOUBLE_STRIKEOUT:
					return "雙刪除線";		
				case OZCStringResource.IDS_OPT_NO_FILL:
					return "無填充";
				case OZCStringResource.IDS_OPT_TRANSPARENCY:
					return "透明度 :";
				case OZCStringResource.IDS_OPT_GRADIENT:
					return "漸變色";	
				case OZCStringResource.IDS_OPT_USE_GRADIENT:
					return "使用漸變色";	
				case OZCStringResource.IDS_OPT_SLASH:
					return "左斜對角線";
				case OZCStringResource.IDS_OPT_BACK_SLASH:
					return "右斜對角線";	
				case OZCStringResource.IDS_OPT_START_POSITION:
					return "起點";	
				case OZCStringResource.IDS_OPT_FROM_EDGE:
					return "邊角";	
				case OZCStringResource.IDS_OPT_DIRECT:
					return "正方向";	
				case OZCStringResource.IDS_OPT_FROM_CENTER:
					return "中心";	
				case OZCStringResource.IDS_OPT_REVERSE:
					return "反方向";					
				case OZCStringResource.IDS_OPT_THICKNESS_W:
					return "厚度(W) :";	
				case OZCStringResource.IDS_OPT_STYLE_Y:
					return "樣式(Y) :";	
				case OZCStringResource.IDS_OPT_COLOR_C:
					return "顏色(C) :";		
				case OZCStringResource.IDS_OPT_CLIPPING:
					return "剪貼";	
				case OZCStringResource.IDS_OPT_WORD_WRAP:
					return "自動換行";	
				case OZCStringResource.IDS_OPT_WORD_WRAP_TYPE:
					return "自動換行方式";	
				case OZCStringResource.IDS_OPT_IGNORE_SPACE:
					return "忽略空白";	
				case OZCStringResource.IDS_OPT_NON_ASCII_WORD:
					return "不允許切斷單詞";	
				case OZCStringResource.IDS_OPT_TOOLTIP_TEXT:
					return "工具提示文本...";						
				case OZCStringResource.IDS_OPT_BASIC:
					return "Basic";
				case OZCStringResource.IDS_OPT_OUTLINE:
					return "Outline";
				case OZCStringResource.IDS_OPT_3D:
					return "3D";
				case OZCStringResource.IDS_OPT_ENGRAVE:
					return "Engrave";
				case OZCStringResource.IDS_OPT_SEGMENT:
					return "Segments";
				case OZCStringResource.IDS_OPT_HOLLOW:
					return "Hollow";
				case OZCStringResource.IDS_OPT_SHADOW:
					return "Shadow";						
				case OZCStringResource.IDS_OPT_IMAGE_STYLE:
					return "樣式 :";		
				case OZCStringResource.IDS_OPT_BARCODE_STYLE:
					return "條碼類型 :";
				case OZCStringResource.IDS_OPT_DATA_GAP:
					return "資料間距：";
				case OZCStringResource.IDS_OPT_DATA_POSITION:
					return "資料位置：";
				case OZCStringResource.IDS_OPT_BARCODE_MARGIN:
					return "條碼邊距：";	
				case OZCStringResource.IDS_OPT_ALIGN_HORIZONTALLY:
					return "條碼水平對齊：";
				case OZCStringResource.IDS_OPT_RATION_COLON:
					return "比例 :";
				case OZCStringResource.IDS_OPT_PRINTING_SCALE:
					return "列印尺寸：";
				case OZCStringResource.IDS_OPT_ROTATION_COLON:
					return "旋轉 :";		
				case OZCStringResource.IDS_OPT_TITLE_GAP:
					return "標題間距：";
				case OZCStringResource.IDS_OPT_TITLE_POSITION:
					return "標題位置：";
				case OZCStringResource.IDS_OPT_BARCODE_ERROR_OPTION:
					return "錯誤顯示選項 :";				
				case OZCStringResource.IDS_OPT_DISPLAY_ERROR_DATA:
					return "顯示錯誤資料";		
				case OZCStringResource.IDS_OPT_DISPLAY_ERROR_SIZE:
					return "顯示條碼大小錯誤";		
				case OZCStringResource.IDS_OPT_DISPLAY_EMPTY_DATA:
					return "顯示空資料";
				case OZCStringResource.IDS_OPT_PDF_IMAGE_STYLE:
					return "圖像類型：";		
				case OZCStringResource.IDS_OPT_PDF_ROW_NUMBER:
					return "行數：";		
				case OZCStringResource.IDS_OPT_PDF_COL_NUMBER:
					return "列數 :";		
				case OZCStringResource.IDS_OPT_PDF_ECC:
					return "修改錯誤 :";							
				case OZCStringResource.IDS_OPT_PDF_X_SCALE:
					return "X大小 :";		
				case OZCStringResource.IDS_OPT_PDF_Y_SCALE:
					return "Y大小 :";						
				case OZCStringResource.IDS_OPT_BORDER_COLOR:
					return "邊緣顏色 :";	
				case OZCStringResource.IDS_OPT_FILL_COLOR:
					return "填充色 :";	
				case OZCStringResource.IDS_OPT_THICKNESS:
					return "厚度 :";	
				case OZCStringResource.IDS_OPT_SHAPE_NO_FILL:
					return "透明 :";	
				case OZCStringResource.IDS_OPT_LINE_TYPE:
					return "線種類 :";	
				case OZCStringResource.IDS_OPT_TOOLTIP_TEXT_COLON:
					return "工具提示文本 :";	
				case OZCStringResource.IDS_OPT_EDIT_CHART:
					return "圖表編輯";	
				case OZCStringResource.IDS_OPT_SHOW_DATA:
					return "查看資料";							
				case OZCStringResource.IDS_OPT_EDIT_SHAPE:
					return "圖形編輯";		
				case OZCStringResource.IDS_FORCS_RV:
					return "Forcs(R) OZ Report Viewer";						
				case OZCStringResource.IDS_OPT_EXCEL_LINE:
					return "線條";					
				case OZCStringResource.IDS_OPT_EXCEL_SAVE_TYPE:
					return "保存形式";	
				case OZCStringResource.IDS_OPT_JPG_ZOOM_INOUT:
					return "放大/縮小";					
				case OZCStringResource.IDS_OPT_ZOOM_INOUT_COLON:
					return "放大/縮小 : ";
				case OZCStringResource.IDS_OPT_QUALITY_COLON:
					return "品質 : ";
				case OZCStringResource.IDS_OPT_TIFF_SIZE:
					return "大小";	
				case OZCStringResource.IDS_OPT_PAGELINE:
					return "Line";
				case OZCStringResource.IDS_OPT_WORD_BAND_SAVE_PAGE:
					return "將頁眉/頁腳Band保存在頁眉/頁腳";	
				case OZCStringResource.IDS_OPT_SAVE_OPTION:
					return "保存方式";	
				case OZCStringResource.IDS_OPT_PRINT_MULTIDOC:
					return "多文檔";	
				case OZCStringResource.ID_PAGE_CONTROL_WINDOW:
					return "選頁窗";							
				case OZCStringResource.IDS_OPT_FONT_NAME_COLON:
					return "字體 :";
				case OZCStringResource.IDS_OPT_FONT_OPTION:
					return "選項";
				case OZCStringResource.IDS_OPT_AUTO_SELECTED:
					return "自動調整";		
				case OZCStringResource.IDS_OPT_SIDE:
					return "寬度";
				case OZCStringResource.IDS_OPT_LENGTH:
					return "高度";						
				case OZCStringResource.IDS_OPT_OTHER:
					return "自定義";	
				case OZCStringResource.IDS_OPT_FONT_SAVE_WAY:
					return "字體";						
				case OZCStringResource.IDS_OPT_HORIZONTAL_NAVI:
					return "水平";										
				case OZCStringResource.IDS_OPT_VERTICAL_NAVI:
					return "垂直";	
				case OZCStringResource.IDS_OPT_PNG_SAVE:
					return "PNG 保存選項";
				case OZCStringResource.IDS_OPT_GIF_SAVE:
					return "GIF 保存選項";	
				case OZCStringResource.IDS_OPT_REMOVE_LINES:
					return "刪除行";						
				case OZCStringResource.IDS_OPT_ENCODING_HDM:
					return "編碼 : ";					
				case OZCStringResource.IDS_OPT_SVG_SAVE:
					return "SVG 保存選項";	
				case OZCStringResource.IDS_OPT_PDF_CHANGEABLE:
					return "複製內容";	
				case OZCStringResource.IDS_OPT_PDF_RADIO_CHANGEABLE1:
					return "無";	
				case OZCStringResource.IDS_OPT_PDF_RADIO_CHANGEABLE2:
					return "注釋, 填寫表單和簽名現有簽名域";	
				case OZCStringResource.IDS_OPT_PDF_RADIO_CHANGEABLE3:
					return "頁面配置, 填寫表單和簽名現有簽名域";	
				case OZCStringResource.IDS_OPT_PDF_RADIO_CHANGEABLE4:
					return "除了提取頁面";		
				case OZCStringResource.IDS_OPT_SAVE_WAY_BASE:
					return "保存方式";		
				case OZCStringResource.IDS_OPT_SAVE_LARGEBUNDLE_BYPAGE:
					return "一頁/分頁保存報表";						
				case OZCStringResource.IDS_OPT_SAVE_LARGE_BUNDLE:
					return "保存為一頁";		
				case OZCStringResource.IDS_OPT_SAVE_BY_PAGE:
					return "分頁保存";	
				case OZCStringResource.IDS_OPT_KEEP_SAVING:
					return "keep saving";		
				case OZCStringResource.IDC_TITLE_SECURITY:
					return "限制打開";		
				case OZCStringResource.IDC_EDIT_PASSWORD:
					return "打開口令";	
				case OZCStringResource.IDC_EDIT_PASSWORD_VERIFY:
					return "確認打開口令";	
				case OZCStringResource.IDC_ZIP_GROUP:
					return "壓縮";	
				case OZCStringResource.IDC_COMPRESS_ON_SAVE:
					return "壓縮保存";
				case OZCStringResource.IDS_OPT_MARGIN_COLON:
					return "邊距 :";	
				case OZCStringResource.IDC_PAPER_SETUP:
					return "紙張設定";					
				case OZCStringResource.IDC_CHECK_ADJUST:
					return "按紙張大小縮放";							
				
				case OZCStringResource.IDS_CHECK_EXCEL_ZOOM:
					return "比例";
				case OZCStringResource.IDS_CHECK_EXCEL_ZOOM_RATIO2:
					return "放大/縮小(%)";					
				case OZCStringResource.IDS_CHECK_EXCEL_ZOOM_RATIO:
					return "放大/縮小: ";							
				case OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES:
					return "Freeze panes";			
				case OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES_ROW:
					return "Row: ";							
				case OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES_COL:
					return "Column: ";						
				case OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES_ROW_M:
					return "Row ";							
				case OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES_COL_M:
					return "Column ";						
			}
		} else {
			switch (id) {
				case OZCStringResource.IDS_OPT_SAVEAS_DEFAULT:
					return "Basic Option";
				case OZCStringResource.IDS_OPT_SAVEAS:
					return "Save Option";
				case OZCStringResource.IDS_OPT_SAVE_WAY_TXT:
					return "Save report after binding per one page/pages.";
				case OZCStringResource.IDS_OPT_SAVE_WAY_ONEPAGE:
					return "Save Per OnePage";
				case OZCStringResource.IDS_OPT_SAVE_WAY_EACHPAGE:
					return "Save Per Pages";		
					
				case OZCStringResource.IDS_OPT_SAVE_WAY:
					return "Save mode";
				case OZCStringResource.IDS_OPT_SAVE_ACTION:
					return "Action";
				case OZCStringResource.IDS_OPT_SAVE_MULTIDOC:
					return "Multi reports";
				case OZCStringResource.IDS_OPT_SAVE_AREA:
					return "Save range";
				case OZCStringResource.IDS_OPT_SAVE_COMPONENT:
					return "Save Components";
				case OZCStringResource.IDS_OPT_SAVE_LINK_INFO:
					return "Save link info.";		
				case OZCStringResource.IDS_OPT_ACTION_AFTER_SAVE:
					return "Action after save";					
				case OZCStringResource.IDS_OPT_SAVE_ONEFILE:
					return "Save as one file";					
				case OZCStringResource.IDS_OPT_SAVE_ALLFILE:
					return "Save all document";		
				case OZCStringResource.IDS_OPT_ALLPAGE:
					return "All";					
				case OZCStringResource.IDS_OPT_CURRENTPAGE:
					return "Current page";					
				case OZCStringResource.IDS_OPT_SELECTEDPAGE:
					return "Selection";		
				case OZCStringResource.IDS_OPT_CHOOSEPAGE:
					return "Pages : ";		
				case OZCStringResource.IDS_OPT_CHOOSEPAGE_DETAIL:
					return "Enter page numbers and/or page ranges separated by commas.";	 				
				case OZCStringResource.IDS_OPT_CHOOSEPAGE_DETAIL2:
					return "For example, 1,3,5-12";					
				case OZCStringResource.IDS_OPT_COMPONENT_LABEL:
					return "Check the components to be saved.";					
				case OZCStringResource.IDS_OPT_LABEL:
					return "Text Label";
				case OZCStringResource.IDS_OPT_IMAGE:
					return "Image";
				case OZCStringResource.IDS_OPT_BARCODE:
					return "Barcode";					
				case OZCStringResource.IDS_OPT_PDF417:
					return "PDF417 Barcode";					
				case OZCStringResource.IDS_OPT_QR_BARCODE:
					return "QR Barcode";					
				case OZCStringResource.IDS_OPT_HTML:
					return "HTML";	
				case OZCStringResource.IDS_OPT_CHART:
					return "Chart";			
				case OZCStringResource.IDS_OPT_LINE:
					return "Line";			
				case OZCStringResource.IDS_OPT_CIRCLE:
					return "Circle";			
				case OZCStringResource.IDS_OPT_RECTANGLE:
					return "Rectangle";			
				case OZCStringResource.IDS_OPT_ARROW:
					return "Arrow";						
				case OZCStringResource.IDS_OPT_URL_LINK:
					return "URL Link";			
				case OZCStringResource.IDS_OPT_LIST_LINK:
					return "TOC Link";		
				case OZCStringResource.IDS_OPT_PDF_SAVE:
					return "PDF Option";		
				case OZCStringResource.IDS_OPT_DOC_IMFOR:
					return "Description(PDF Information)";			
				case OZCStringResource.IDS_OPT_OPEN_PROTECT:
					return "Open Restriction";					
				case OZCStringResource.IDS_OPT_EDIT_PROTECT:
					return "Editing Restriction";
				case OZCStringResource.IDS_OPT_USER_RIGHT:
					return "User access permissions";							
				case OZCStringResource.IDS_OPT_TITLE:
					return "Title";
				case OZCStringResource.IDS_OPT_WRITER:
					return "Author";			
				case OZCStringResource.IDS_OPT_SUBJECT:
					return "Subject";			
				case OZCStringResource.IDS_OPT_KEYWORD:
					return "Keywords";
				case OZCStringResource.IDS_OPT_APPLICATION:
					return "Application";			
				case OZCStringResource.IDS_OPT_USER_PW:
					return "Open password";
				case OZCStringResource.IDS_OPT_USER_PW_CONFIRM:
					return "Verify password";						
				case OZCStringResource.IDS_OPT_MASTER_PW:
					return "Permissions Password";
				case OZCStringResource.IDS_OPT_MASTER_PW_CONFIRM:
					return "Verify Password";		
				case OZCStringResource.IDS_OPT_PRINT_PERMIT:
					return "Printing allowed";
				case OZCStringResource.IDS_OPT_CONTENTS_COPY:
					return "Copy allowed";
				case OZCStringResource.IDS_OPT_SAVEIMG_TOCHART:
					return "Save chart to image";
				case OZCStringResource.IDS_OPT_FONT_INCLUDE:
					return "폰트 포함";		
				case OZCStringResource.IDS_OPT_PRINT:
					return "Print";
				case OZCStringResource.IDS_OPT_PRINT_COUNT:
					return "Copies";		
				case OZCStringResource.IDS_OPT_PRINT_SCOPE:
					return "Print range";
				case OZCStringResource.IDS_OPT_PRINT_RULE:
					return "Print mode";					
				case OZCStringResource.IDS_OPT_COUNT:
					return "copies";
				case OZCStringResource.IDS_OPT_PRINT_ONEPAGE:
					return "Collate";
				case OZCStringResource.IDS_OPT_PRINT_ALL_REPORT:
					return "Print all reports";					
				case OZCStringResource.IDS_OPT_ALL:
					return "All";					
				case OZCStringResource.IDS_OPT_CHOOSE_PAGE:
					return "Pages";	
				case OZCStringResource.IDS_OPT_PRINT_FITTOPAGE:
					return "Adjust printing scale";			
				case OZCStringResource.IDS_OPT_DEVIDE_PRINT:
					return "Divide";
				case OZCStringResource.IDS_OPT_DEVIDE_PRINT_TOFITPAPER:
					return "Print divide";					
				case OZCStringResource.IDS_TOGETHER_PRINT:
					return "Handouts";					
				case OZCStringResource.IDS_OPT_PAGECOUNT_TOPRINT:
					return "Pages per paper";	
				case OZCStringResource.IDS_OPT_PRINT_ORDER:
					return "Order";										
				case OZCStringResource.IDS_OPT_HORIZON:
					return "Horizontal";										
				case OZCStringResource.IDS_OPT_VERTICAL:
					return "Vertical";										
				case OZCStringResource.IDS_OPT_PRINT_DIRECTION:
					return "Orientation";										
				case OZCStringResource.IDS_OPT_PORTRAIT:
					return "Portrait";										
				case OZCStringResource.IDS_OPT_LANDSCAPE:
					return "Landscape";								
				case OZCStringResource.IDS_OPT_OPTION:
					return "Option";		
				case OZCStringResource.IDS_OPT_CONFIRM:
					return "OK";		
				case OZCStringResource.IDS_OPT_CANCEL:
					return "Cancel";							
				case OZCStringResource.IDS_OPT_SAVE:
					return "저장";
				case OZCStringResource.IDS_OPT_OZD_SAVE_OPTION:
					return "Save Option";								
				case OZCStringResource.IDS_OPT_PASSWORD:
					return "Password";		
				case OZCStringResource.IDS_OPT_MEMOADD_OK:
					return "Include edits";		
				case OZCStringResource.IDS_OPT_SAVE_ALLREPORT:
					return "Save all reports";						
				case OZCStringResource.IDS_OPT_INCLUDE_IMG:
					return "Include Image";						
				case OZCStringResource.IDS_OPT_FILE_TYPE:
					return "Save as type";				
				case OZCStringResource.IDS_OPT_EXCEL_SAVE:
					return "Excel Option";
				case OZCStringResource.IDS_OPT_EXCEL_SAVE2:
					return "Excel Option(advanced)";					
				case OZCStringResource.IDS_OPT_NEXCEL_SAVE:
					return "Nexcel Option";
				case OZCStringResource.IDS_OPT_WORD_SAVE:
					return "Word Option";						
				case OZCStringResource.IDS_OPT_PPT_SAVE:
					return "PowerPoint Option";		
				case OZCStringResource.IDS_OPT_HTML_SAVE:
					return "HTML Option";		
				case OZCStringResource.IDS_OPT_CSV_SAVE:
					return "CSV Option";		
				case OZCStringResource.IDS_OPT_TEXT_SAVE:
					return "TEXT Option";		
				case OZCStringResource.IDS_OPT_JPG_SAVE:
					return "JPG Option";
				case OZCStringResource.IDS_OPT_TIFF_SAVE:
					return "TIFF Option";		
				case OZCStringResource.IDS_OPT_HWP_SAVE:
					return "Hangul Option";		
				case OZCStringResource.IDS_OPT_HWP97_SAVE:
					return "Hangul97 Option";		
				case OZCStringResource.IDS_OPT_MHT_SAVE:
					return "MHT Option";
				case OZCStringResource.IDS_OPT_SAVE_FORM:
					return "Format";//"Save format";							
				case OZCStringResource.IDS_OPT_PAGE:
					return "Page";
				case OZCStringResource.IDS_OPT_FONT:
					return "Font";
				case OZCStringResource.IDS_OPT_FORMAT:
					return "Format";
				case OZCStringResource.IDS_OPT_AUTO_FIT:
					return "Auto select";
				case OZCStringResource.IDS_OPT_CELL_MINIMUM_SIZE_CELL:
					return "Minimum cell size";
				case OZCStringResource.IDS_OPT_SAVE_MODE:
					return "Save format";			
				case OZCStringResource.IDS_OPT_KEEP_FORM_ATTR:
					return "Keep the report form";			
				case OZCStringResource.IDS_OPT_IGNORE_COLOR_OUTLINE_CELL:
					return "Ignore color, border, merge cell";
				case OZCStringResource.IDS_OPT_REMOVE_CELL_AFTERDIVIDE:
					return "Remove adjacent cell after dividing";
				case OZCStringResource.IDS_OPT_SAVE_SHEET_PER_COLPAGE:
					return "Column pages per sheet";
				case OZCStringResource.IDS_OPT_SAVE_SHEET_PAGE:
					return "Page per sheet";
				case OZCStringResource.IDS_OPT_COLUMN_FIRST:
					return "Column first";
				case OZCStringResource.IDS_OPT_ROW_FIRST:
					return "Row first";					
				case OZCStringResource.IDS_OPT_REMOVE_PAGE_GAP:
					return "Remove blank lines between pages";
				case OZCStringResource.IDS_OPT_SIZE_COLON:
					return "Size:";
				case OZCStringResource.IDS_OPT_REMOVE_LINE:
					return "Remove line";		
				case OZCStringResource.IDS_OPT_EXCLUDE_FIRST_PAGE:
					return "Except first page";
				case OZCStringResource.IDS_OPT_ONLY_FIRST_PAGE:
					return "Only first page";
				case OZCStringResource.IDS_OPT_ALL_PAGE:
					return "All pages";						
				case OZCStringResource.IDS_OPT_LINE_SCOPE_LINE_NUMBER:
					return "Enter Line ranges and/or line numbers separated by commas, For example, 1-3,5,12";					
				case OZCStringResource.IDS_OPT_CHANGE_TXT_TO_INTEGER:
					return "Numeric type text convert to number";
				case OZCStringResource.IDS_OPT_APPLY_ONLYLABEL_NUMBER:
					return "Apply to numeric format label only";
				case OZCStringResource.IDS_OPT_CHANGE_AS_NORMAL:
					return "Convert data to general(cell format)";				
				case OZCStringResource.IDS_OPT_FONT_COLON:
					return "Font:";					
				case OZCStringResource.IDS_OPT_WIDTH:
					return "Width";
				case OZCStringResource.IDS_OPT_HEIGHT:
					return "Height";
				case OZCStringResource.IDS_OPT_SAVE_MHT_TYPE:
					return "Save as mht (Supports Office XP and up)";					
				case OZCStringResource.IDS_OPT_TABLE_SAVE_WAY:
					return "Table save mode";
				case OZCStringResource.IDS_OPT_TABLE_TYPE_WAY:
					return "Save As Table           ";
				case OZCStringResource.IDS_OPT_BAND_SAVE_WAY:
					return "Band save mode";
				case OZCStringResource.IDS_OPT_BAND_SAVE_PAGE:
					return "Save page header/footer band to header/footer";				
				case OZCStringResource.IDS_OPT_PPT_SAVE_WAY:
					return "PowerPoint save mode";
				case OZCStringResource.IDS_OPT_MHTD_SAVE_PAGE:
					return "Save as mht (Supports Office XP and up)";		
				case OZCStringResource.IDS_OPT_FONT_SIZE_SETTING:
					return "Set font size";
				case OZCStringResource.IDS_OPT_PAGE_BETWEEN:
					return "Page spacing";
				case OZCStringResource.IDS_OPT_PER_PAGE_SAVE:
					return "Save each page to different file.";								
				case OZCStringResource.IDS_OPT_LINE_COUNT_COLON:
					return "Lines :";
				case OZCStringResource.IDS_OPT_OFFSET_COLON:
					return "Offset:";
				case OZCStringResource.IDS_OPT_STARTPOINT_HTMLDOC:
					return "Set the start coordinate of HTML document.";
				case OZCStringResource.IDS_OPT_ENCODING_COLON:
					return "Encoding : ";
				case OZCStringResource.IDS_OPT_X_AXIS:
					return "X axis";							
				case OZCStringResource.IDS_OPT_Y_AXIS:
					return "Y axis";
				case OZCStringResource.IDS_OPT_OFFSET:
					return "Offset";			
				case OZCStringResource.IDS_OPT_SEPARATION:
					return "Field delimiter";							
				case OZCStringResource.IDS_OPT_SEPARATOR:
					return "Delimiter";	
				case OZCStringResource.IDS_OPT_USER_DEFINE:
					return "User Define";						
				case OZCStringResource.IDS_OPT_SEPARATION_TYPE:
					return "Break type";
				case OZCStringResource.IDS_OPT_PAGE_BRACKET:
					return "<PAGE>";
				case OZCStringResource.IDS_OPT_SET_PAGELINE:
					return "Set the lines between pages.";
				case OZCStringResource.IDS_OPT_CHAR_NUMBER:
					return "Numeric data";					
				case OZCStringResource.IDS_OPT_ADD_SEPARATOR:
					return "Insert 1000 separator";					
				case OZCStringResource.IDS_OPT_REMOVE_SEPARATOR:
					return "Delete 1000 separator";
				case OZCStringResource.IDS_OPT_PAGE_SEPARATION:
					return "Page number break";						
				case OZCStringResource.IDS_OPT_LABEL_SAVE:
					return "Label save mode";		
				case OZCStringResource.IDS_OPT_LABEL_SIZE:
					return "Keep label size";
				case OZCStringResource.IDS_OPT_POSITION_SAVE:
					return "Location save mode";
				case OZCStringResource.IDS_OPT_VERTREL_TOPARA:
					return "From the paragraph";
				case OZCStringResource.IDS_OPT_BACKGROUND_BAND:
					return "Save background band to background page";								
				case OZCStringResource.IDS_OPT_TITLE_COLON:
					return "Title :";					
				case OZCStringResource.IDS_OPT_DISPLAY_OPTION:
					return "Display option";	
				case OZCStringResource.IDS_OPT_GRIDLINES:
					return "Show gridlines";
				case OZCStringResource.IDS_OPT_ENCODING_WAY:
					return "Encoding";
				case OZCStringResource.IDS_OPT_ENCODING_G3:
					return "G3";	
				case OZCStringResource.IDS_OPT_ENCODING_G4:
					return "G4";		
				case OZCStringResource.IDS_OPT_ENCODING_SAVEAS_ONEFILE:
					return "MultiPage save as one file";
				case OZCStringResource.IDS_OPT_RATIO:
					return "Ratio";	
				case OZCStringResource.IDS_OPT_ZOOM_INOUT:
					return "Zoom In/Out";
				case OZCStringResource.IDS_OPT_SET_DPI:
					return "Set size by DPI";   
				case OZCStringResource.IDS_OPT_DPI:
					return "DPI";	
				case OZCStringResource.IDS_OPT_SIZE:
					return "Size";	
				case OZCStringResource.IDS_OPT_SAVE_RATIO:
					return "Save Ratio";
				case OZCStringResource.IDS_OPT_COMPRESSION:
					return "Compression";
				case OZCStringResource.IDS_OPT_QUALITY:
					return "Quality";				
				case OZCStringResource.IDS_OPT_USE_INDENT:
					return "Use indentation";	
				case OZCStringResource.IDS_OPT_DATA_SAVE:
					return "HDM Option";		
				case OZCStringResource.IDS_OPT_FIND_WHAT:
					return "Find What:";	
				case OZCStringResource.IDS_OPT_MATCH_WHOLE_WORD:
					return "Match Whole word";
				case OZCStringResource.IDS_OPT_MATCH_CASE:
					return "Match case";
				case OZCStringResource.IDS_OPT_DIRECTION:
					return "Direction";			
				case OZCStringResource.IDS_OPT_SEARCH_UP:
					return "Up";
				case OZCStringResource.IDS_OPT_SEARCH_DOWN:
					return "Down";
				case OZCStringResource.IDS_OPT_NEXT:
					return "Find Next";		
				case OZCStringResource.IDS_OPT_REFRESH_REPORT:
					return "Refresh Report";		
				case OZCStringResource.IDS_OPT_INTERVAL:
					return "Interval";	
				case OZCStringResource.IDS_OPT_HH:
					return "hh";		
				case OZCStringResource.IDS_OPT_MM:
					return "mm";		
				case OZCStringResource.IDS_OPT_SS:
					return "ss";	
				case OZCStringResource.IDS_OPT_REFRESH_NOW:
					return "Refresh now";		
				case OZCStringResource.IDS_OPT_START:
					return "Start";		
				case OZCStringResource.IDS_OPT_STOP:
					return "Stop";			
				case OZCStringResource.IDS_OPT_LEFT_COLON:
					return "Left:";
				case OZCStringResource.IDS_OPT_RIGHT_COLON:
					return "Right:";
				case OZCStringResource.IDS_OPT_TOP_COLON:
					return "Top:";
				case OZCStringResource.IDS_OPT_BOTTOM_COLON:
					return "Bottom:";	
				case OZCStringResource.IDS_OPT_PAGE_SETUP:
					return "Page Setup";	
				case OZCStringResource.IDS_OPT_MARGIN:
					return "Margin";	
				case OZCStringResource.IDS_OPT_UNIT:
					return "Unit";		
				case OZCStringResource.IDS_OPT_UNIT_COLON:
					return "Unit:";	
				case OZCStringResource.IDS_OPT_MARGIN_OPTION:
					return "Margin Option";		
				case OZCStringResource.IDS_OPT_MARGIN_DEFAULT:
					return "Use Default";	
				case OZCStringResource.IDS_OPT_MARGIN_AUTO_ADJUST:
					return "Auto adjust margin";	
				case OZCStringResource.IDS_OPT_HTML_VERSION:
					return "Version";		
				case OZCStringResource.IDS_OPT_HTML_VERSION_COLON:
					return "HTML Version :";
				case OZCStringResource.IDS_OPT_TOTAL:
					return "Total";	
				case OZCStringResource.IDS_OPT_REPORT_STRUCTURE:
					return "Show Report Structure";				
				case OZCStringResource.IDS_OPT_LABEL_FORMAT:
					return "Format";			
				case OZCStringResource.IDS_OPT_PARAGRAPH:
					return "Paragraph";			
				case OZCStringResource.IDS_OPT_FILL:
					return "Fill";
				case OZCStringResource.IDS_OPT_LINECOLOR:
					return "Line Color";	
				case OZCStringResource.IDS_OPT_ETC:
					return "Etc";	
				case OZCStringResource.IDS_OPT_ALIGNMENT:
					return "Alignment";		
				case OZCStringResource.IDS_OPT_TXT_DIRECTION:
					return "Text Direction";					
				case OZCStringResource.IDS_OPT_INTERNAL_MARGIN:
					return "Internal Margin";		
				case OZCStringResource.IDS_OPT_PREVIEW:
					return "Preview";						
				case OZCStringResource.IDS_OPT_HORIZON_COLON:
					return "Horizontal :";										
				case OZCStringResource.IDS_OPT_VERTICAL_COLON:
					return "Vertical :";	
				case OZCStringResource.IDS_OPT_LINE_SPACE_COLON:
					return "Line Space :";										
				case OZCStringResource.IDS_OPT_INDENT_COLON:
					return "Indent :";								
				case OZCStringResource.ID_OPT_PARAMETERS_TITLE_LABEL:
					return "Chage value of parameters.";
				case OZCStringResource.IDS_OPT_EXIT:
					return "Exit";
				case OZCStringResource.IDS_OPT_OZZPASSWORD:
					return "Password";		
				case OZCStringResource.IDS_OPT_OZZPASSWORD_CHECK:
					return "Confirm Password";						
				case OZCStringResource.IDS_OPT_ID:
					return "ID";		
				case OZCStringResource.IDS_OPT_SECURITY:
					return "Security";		
				case OZCStringResource.IDS_OPT_PRODUCT_INFO:
					return "Product Info.";
				case OZCStringResource.IDS_OPT_OZ_INFO:
					return "About";			
				case OZCStringResource.IDS_OPT_ERROR_CODE_COLON:
					return "Error code:";			
				case OZCStringResource.IDS_OPT_GENERAL_MSG_COLON:
					return "General message:";		
				case OZCStringResource.IDS_OPT_VIEW_DETAIL:
					return "View detail";		
				case OZCStringResource.IDS_OPT_DETAIL_MSG_COLON:
					return "Detail message:";	
				case OZCStringResource.IDS_OPT_TEXT_CONTROL:
					return "Text control";							
				case OZCStringResource.IDS_OPT_SHRINK_TO_FIT:
					return "Shrink to fit";
				case OZCStringResource.IDS_OPT_PASSWORD2:
					return "Confirm";		
				case OZCStringResource.IDS_OPT_DM_BARCODE:
					return "DataMatrix Barcode";		
				case OZCStringResource.IDS_OPT_FONT_STYLE_COLON:
					return "Style :";
				case OZCStringResource.IDS_OPT_COLOR_COLON:
					return "Color :";
				case OZCStringResource.IDS_OPT_INTERVAL_COLON:
					return "Interval :";					
				case OZCStringResource.IDS_OPT_STRETCH_COLON:
					return "Stretch :";					
				case OZCStringResource.IDS_OPT_EFFECT:
					return "Effect";	
				case OZCStringResource.IDS_OPT_UNDERLINE:
					return "Underline";					
				case OZCStringResource.IDS_OPT_STRIKEOUT:
					return "Strikeout";					
				case OZCStringResource.IDS_OPT_DOUBLE_STRIKEOUT:
					return "Double Strikeout";		
				case OZCStringResource.IDS_OPT_NO_FILL:
					return "No Fill";
				case OZCStringResource.IDS_OPT_TRANSPARENCY:
					return "Transparency";
				case OZCStringResource.IDS_OPT_GRADIENT:
					return "Gradient Style";	
				case OZCStringResource.IDS_OPT_USE_GRADIENT:
					return "Use Gradient";	
				case OZCStringResource.IDS_OPT_SLASH:
					return "Slash";
				case OZCStringResource.IDS_OPT_BACK_SLASH:
					return "Back Slash";	
				case OZCStringResource.IDS_OPT_START_POSITION:
					return "Start Position";	
				case OZCStringResource.IDS_OPT_FROM_EDGE:
					return "From Edge";	
				case OZCStringResource.IDS_OPT_DIRECT:
					return "Direct";	
				case OZCStringResource.IDS_OPT_FROM_CENTER:
					return "From Center";	
				case OZCStringResource.IDS_OPT_REVERSE:
					return "Reverse";		
				case OZCStringResource.IDS_OPT_THICKNESS_W:
					return "Thickness(W) :";	
				case OZCStringResource.IDS_OPT_STYLE_Y:
					return "Style(Y) :";	
				case OZCStringResource.IDS_OPT_COLOR_C:
					return "Color(C) :";	
				case OZCStringResource.IDS_OPT_CLIPPING:
					return "Clipping";	
				case OZCStringResource.IDS_OPT_WORD_WRAP:
					return "Word Wrap";	
				case OZCStringResource.IDS_OPT_WORD_WRAP_TYPE:
					return "Word Wrap Type";	
				case OZCStringResource.IDS_OPT_IGNORE_SPACE:
					return "Ignore Space";	
				case OZCStringResource.IDS_OPT_NON_ASCII_WORD:
					return "Non Ascii Word";	
				case OZCStringResource.IDS_OPT_TOOLTIP_TEXT:
					return "Tooltip Text...";
				case OZCStringResource.IDS_OPT_BASIC:
					return "Basic";
				case OZCStringResource.IDS_OPT_OUTLINE:
					return "Outline";
				case OZCStringResource.IDS_OPT_3D:
					return "3D";
				case OZCStringResource.IDS_OPT_ENGRAVE:
					return "Engrave";
				case OZCStringResource.IDS_OPT_SEGMENT:
					return "Segments";
				case OZCStringResource.IDS_OPT_HOLLOW:
					return "Hollow";
				case OZCStringResource.IDS_OPT_SHADOW:
					return "Shadow";	
				case OZCStringResource.IDS_OPT_IMAGE_STYLE:
					return "Image Style :";			
				case OZCStringResource.IDS_OPT_BARCODE_STYLE:
					return "Style :";
				case OZCStringResource.IDS_OPT_DATA_GAP:
					return "Data Gap ：";
				case OZCStringResource.IDS_OPT_DATA_POSITION:
					return "Data Position :";
				case OZCStringResource.IDS_OPT_BARCODE_MARGIN:
					return "Barcode Margin :";	
				case OZCStringResource.IDS_OPT_ALIGN_HORIZONTALLY:
					return "Align Horizontally :";
				case OZCStringResource.IDS_OPT_RATION_COLON:
					return "Ratio :";
				case OZCStringResource.IDS_OPT_PRINTING_SCALE:
					return "Printing Scale :";
				case OZCStringResource.IDS_OPT_ROTATION_COLON:
					return "Rotation :";		
				case OZCStringResource.IDS_OPT_TITLE_GAP:
					return "Title Gap :";
				case OZCStringResource.IDS_OPT_TITLE_POSITION:
					return "Title Position :";
				case OZCStringResource.IDS_OPT_BARCODE_ERROR_OPTION:
					return "Barcode Error Option :";	
				case OZCStringResource.IDS_OPT_DISPLAY_ERROR_DATA:
					return "Display Error Data";		
				case OZCStringResource.IDS_OPT_DISPLAY_ERROR_SIZE:
					return "Display Error Size";		
				case OZCStringResource.IDS_OPT_DISPLAY_EMPTY_DATA:
					return "Display Empty Data";
				case OZCStringResource.IDS_OPT_PDF_IMAGE_STYLE:
					return "Image Style:";		
				case OZCStringResource.IDS_OPT_PDF_ROW_NUMBER:
					return "Row Number:";		
				case OZCStringResource.IDS_OPT_PDF_COL_NUMBER:
					return "Column Number:";		
				case OZCStringResource.IDS_OPT_PDF_ECC:
					return "ECC:";							
				case OZCStringResource.IDS_OPT_PDF_X_SCALE:
					return "X Scale:";		
				case OZCStringResource.IDS_OPT_PDF_Y_SCALE:
					return "Y Scale:";									
				case OZCStringResource.IDS_OPT_BORDER_COLOR:
					return "Border Color :";	
				case OZCStringResource.IDS_OPT_FILL_COLOR:
					return "Fill Color :";	
				case OZCStringResource.IDS_OPT_THICKNESS:
					return "Thickness :";	
				case OZCStringResource.IDS_OPT_SHAPE_NO_FILL:
					return "No Fill :";	
				case OZCStringResource.IDS_OPT_LINE_TYPE:
					return "Line Type :";	
				case OZCStringResource.IDS_OPT_TOOLTIP_TEXT_COLON:
					return "Tooltip Text :";		
				case OZCStringResource.IDS_OPT_EDIT_CHART:
					return "Edit Chart";	
				case OZCStringResource.IDS_OPT_SHOW_DATA:
					return "Show data";				
				case OZCStringResource.IDS_OPT_EDIT_SHAPE:
					return "Edit Shape";			
				case OZCStringResource.IDS_FORCS_RV:
					return "Forcs(R) OZ Report Viewer";		
				case OZCStringResource.IDS_OPT_EXCEL_LINE:
					return "Line";			
				case OZCStringResource.IDS_OPT_EXCEL_SAVE_TYPE:
					return "Save type";	
				case OZCStringResource.IDS_OPT_JPG_ZOOM_INOUT:
					return "Zoom In/Out";					
				case OZCStringResource.IDS_OPT_ZOOM_INOUT_COLON:
					return "Zoom In/Out : ";
				case OZCStringResource.IDS_OPT_QUALITY_COLON:
					return "Quality : ";
				case OZCStringResource.IDS_OPT_TIFF_SIZE:
					return "Size";	
				case OZCStringResource.IDS_OPT_PAGELINE:
					return "Line";
				case OZCStringResource.IDS_OPT_WORD_BAND_SAVE_PAGE:
					return "Save page header/footer band to header/footer";					
				case OZCStringResource.IDS_OPT_SAVE_OPTION:
					return "Save Option";
				case OZCStringResource.IDS_OPT_PRINT_MULTIDOC:
					return "Multi reports";	
				case OZCStringResource.ID_PAGE_CONTROL_WINDOW:
					return "Page coordinate window";		
				case OZCStringResource.IDS_OPT_FONT_NAME_COLON:
					return "Font Name :";
				case OZCStringResource.IDS_OPT_FONT_OPTION:
					return "Font Option";			
				case OZCStringResource.IDS_OPT_AUTO_SELECTED:
					return "Auto selected";		
				case OZCStringResource.IDS_OPT_SIDE:
					return "Width";
				case OZCStringResource.IDS_OPT_LENGTH:
					return "Height";	
				case OZCStringResource.IDS_OPT_OTHER:
					return "User define";	
				case OZCStringResource.IDS_OPT_FONT_SAVE_WAY:
					return "Font";							
				case OZCStringResource.IDS_OPT_HORIZONTAL_NAVI:
					return "Horizontal";										
				case OZCStringResource.IDS_OPT_VERTICAL_NAVI:
					return "Vertical";	
				case OZCStringResource.IDS_OPT_PNG_SAVE:
					return "PNG Option";
				case OZCStringResource.IDS_OPT_GIF_SAVE:
					return "GIF Option";				
				case OZCStringResource.IDS_OPT_REMOVE_LINES:
					return "Remove lines";	
				case OZCStringResource.IDS_OPT_ENCODING_HDM:
					return "Encoding : ";					
				case OZCStringResource.IDS_OPT_SVG_SAVE:
					return "SVG Option";
				case OZCStringResource.IDS_OPT_PDF_CHANGEABLE:
					return "Changes Allowed";	
				case OZCStringResource.IDS_OPT_PDF_RADIO_CHANGEABLE1:
					return "None";	
				case OZCStringResource.IDS_OPT_PDF_RADIO_CHANGEABLE2:
					return "Commenting, filling in form fields, and signing";	
				case OZCStringResource.IDS_OPT_PDF_RADIO_CHANGEABLE3:
					return "Page layout, filling in form fields, and signing";	
				case OZCStringResource.IDS_OPT_PDF_RADIO_CHANGEABLE4:
					return "Any except extracting of pages";							
				case OZCStringResource.IDS_OPT_SAVE_WAY_BASE:
					return "Save mode";		
				case OZCStringResource.IDS_OPT_SAVE_LARGEBUNDLE_BYPAGE:
					return "A report is bound in a page then it is saved.";						
				case OZCStringResource.IDS_OPT_SAVE_LARGE_BUNDLE:
					return "Save one page";		
				case OZCStringResource.IDS_OPT_SAVE_BY_PAGE:
					return "Save by page";	
				case OZCStringResource.IDS_OPT_KEEP_SAVING:
					return "keep saving";		
				case OZCStringResource.IDC_TITLE_SECURITY:
					return "Restriction";		
				case OZCStringResource.IDC_EDIT_PASSWORD:
					return "Password";	
				case OZCStringResource.IDC_EDIT_PASSWORD_VERIFY:
					return "Verify Password";		//confirm password					
				case OZCStringResource.IDC_ZIP_GROUP:
					return "Compress";	
				case OZCStringResource.IDC_COMPRESS_ON_SAVE:
					return "Compress on save";							
				case OZCStringResource.IDS_OPT_MARGIN_COLON:
					return "Margin :";		
				case OZCStringResource.IDC_PAPER_SETUP:
					return "Change paper size";					
				case OZCStringResource.IDC_CHECK_ADJUST:
					return "Adjust printing scale";							

				case OZCStringResource.IDS_CHECK_EXCEL_ZOOM:
					return "Ratio";
				case OZCStringResource.IDS_CHECK_EXCEL_ZOOM_RATIO2:
					return "Zoom In/Out(%)";					
				case OZCStringResource.IDS_CHECK_EXCEL_ZOOM_RATIO:
					return "Zoom In/Out: ";							
				case OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES:
					return "Freeze panes";				
				case OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES_ROW:
					return "Row: ";							
				case OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES_COL:
					return "Column: ";							
				case OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES_ROW_M:
					return "Row ";							
				case OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES_COL_M:
					return "Column ";							
			}
		}
		return "";
	};
	__static_import__("CLocale");
	OZCStringResource.IDS_OPT_SAVEAS_DEFAULT = 1;
	OZCStringResource.IDS_OPT_SAVEAS = 2;
	OZCStringResource.IDS_OPT_SAVE_WAY_TXT = 3;
	OZCStringResource.IDS_OPT_SAVE_WAY_ONEPAGE = 4;
	OZCStringResource.IDS_OPT_SAVE_WAY_EACHPAGE = 5;
	
	OZCStringResource.IDS_OPT_SAVE_WAY = 6;
	OZCStringResource.IDS_OPT_SAVE_ACTION = 7;
	OZCStringResource.IDS_OPT_SAVE_MULTIDOC = 8;
	OZCStringResource.IDS_OPT_SAVE_AREA = 9;
	OZCStringResource.IDS_OPT_SAVE_COMPONENT = 10;
	OZCStringResource.IDS_OPT_SAVE_LINK_INFO = 11;
	OZCStringResource.IDS_OPT_ACTION_AFTER_SAVE = 12;
	OZCStringResource.IDS_OPT_SAVE_ONEFILE = 13;
	OZCStringResource.IDS_OPT_SAVE_ALLFILE = 14;
	OZCStringResource.IDS_OPT_ALLPAGE = 15;
	OZCStringResource.IDS_OPT_CURRENTPAGE = 16;
	OZCStringResource.IDS_OPT_SELECTEDPAGE = 17;
	OZCStringResource.IDS_OPT_CHOOSEPAGE = 18;
	OZCStringResource.IDS_OPT_COMPONENT_LABEL = 19;

	OZCStringResource.IDS_OPT_LABEL = 20;
	OZCStringResource.IDS_OPT_IMAGE = 21;
	OZCStringResource.IDS_OPT_BARCODE = 22;
	OZCStringResource.IDS_OPT_PDF417 = 23;
	OZCStringResource.IDS_OPT_QR_BARCODE = 24;
	OZCStringResource.IDS_OPT_HTML = 25;
	OZCStringResource.IDS_OPT_CHART = 26;
	OZCStringResource.IDS_OPT_LINE = 27;
	OZCStringResource.IDS_OPT_CIRCLE = 28;
	OZCStringResource.IDS_OPT_RECTANGLE = 29;
	OZCStringResource.IDS_OPT_ARROW = 30;
	OZCStringResource.IDS_OPT_URL_LINK = 31;
	OZCStringResource.IDS_OPT_LIST_LINK = 32;

	OZCStringResource.IDS_OPT_PDF_SAVE = 33;
	OZCStringResource.IDS_OPT_DOC_IMFOR = 34;
	OZCStringResource.IDS_OPT_OPEN_PROTECT = 35;
	OZCStringResource.IDS_OPT_EDIT_PROTECT = 36;
	OZCStringResource.IDS_OPT_USER_RIGHT = 37;
	OZCStringResource.IDS_OPT_TITLE = 38;
	OZCStringResource.IDS_OPT_WRITER = 39;
	OZCStringResource.IDS_OPT_SUBJECT = 40;
	OZCStringResource.IDS_OPT_KEYWORD = 41;
	OZCStringResource.IDS_OPT_APPLICATION = 42;
	OZCStringResource.IDS_OPT_USER_PW = 43;
	OZCStringResource.IDS_OPT_USER_PW_CONFIRM = 44;
	OZCStringResource.IDS_OPT_MASTER_PW = 45;
	OZCStringResource.IDS_OPT_MASTER_PW_CONFIRM = 46;
	OZCStringResource.IDS_OPT_PRINT_PERMIT = 47;
	OZCStringResource.IDS_OPT_CONTENTS_COPY = 48;
	OZCStringResource.IDS_OPT_SAVEIMG_TOCHART = 49;
	OZCStringResource.IDS_OPT_FONT_INCLUDE = 50;

	OZCStringResource.IDS_OPT_PRINT = 51;
	OZCStringResource.IDS_OPT_PRINT_COUNT = 52;
	OZCStringResource.IDS_OPT_PRINT_SCOPE = 53;
	OZCStringResource.IDS_OPT_PRINT_RULE = 54;
	OZCStringResource.IDS_OPT_COUNT = 55;
	OZCStringResource.IDS_OPT_PRINT_ONEPAGE = 56;
	OZCStringResource.IDS_OPT_PRINT_ALL_REPORT = 57;
	OZCStringResource.IDS_OPT_ALL = 58;
	OZCStringResource.IDS_OPT_CHOOSE_PAGE = 59;
	OZCStringResource.IDS_OPT_PRINT_FITTOPAGE = 60;
	
	OZCStringResource.IDS_OPT_DEVIDE_PRINT = 61;
	OZCStringResource.IDS_OPT_DEVIDE_PRINT_TOFITPAPER = 62;
	OZCStringResource.IDS_TOGETHER_PRINT = 63;
	OZCStringResource.IDS_OPT_PAGECOUNT_TOPRINT = 64;
	OZCStringResource.IDS_OPT_PRINT_ORDER = 65;
	OZCStringResource.IDS_OPT_HORIZON = 66;
	OZCStringResource.IDS_OPT_VERTICAL = 67;
	OZCStringResource.IDS_OPT_PRINT_DIRECTION = 68;
	OZCStringResource.IDS_OPT_PORTRAIT = 69;
	OZCStringResource.IDS_OPT_LANDSCAPE = 70;
	OZCStringResource.IDS_OPT_OPTION = 71;
	OZCStringResource.IDS_OPT_CONFIRM = 72;
	OZCStringResource.IDS_OPT_CANCEL = 73;
	OZCStringResource.IDS_OPT_SAVE = 74;
	OZCStringResource.IDS_OPT_OZD_SAVE_OPTION = 75;
	OZCStringResource.IDS_OPT_PASSWORD = 76;
	OZCStringResource.IDS_OPT_MEMOADD_OK = 77;
	OZCStringResource.IDS_OPT_SAVE_ALLREPORT = 78;
	OZCStringResource.IDS_OPT_INCLUDE_IMG = 79;	
	OZCStringResource.IDS_OPT_FILE_TYPE = 80;
	OZCStringResource.IDS_OPT_EXCEL_SAVE = 81;
    OZCStringResource.IDS_OPT_WORD_SAVE = 82;
    OZCStringResource.IDS_OPT_PPT_SAVE = 83;
    OZCStringResource.IDS_OPT_HTML_SAVE = 84;
    OZCStringResource.IDS_OPT_CSV_SAVE = 85;
    OZCStringResource.IDS_OPT_TEXT_SAVE = 86;
    OZCStringResource.IDS_OPT_JPG_SAVE = 87;
    OZCStringResource.IDS_OPT_TIFF_SAVE = 88;
    OZCStringResource.IDS_OPT_HWP_SAVE = 89;
    OZCStringResource.IDS_OPT_HWP97_SAVE = 90;
    OZCStringResource.IDS_OPT_MHT_SAVE = 91;
    OZCStringResource.IDS_OPT_SAVE_FORM = 92;
    OZCStringResource.IDS_OPT_PAGE = 93;
    OZCStringResource.IDS_OPT_FONT = 94;
    OZCStringResource.IDS_OPT_FORMAT = 95;
	OZCStringResource.IDS_OPT_AUTO_FIT = 96;
	OZCStringResource.IDS_OPT_CELL_MINIMUM_SIZE_CELL = 97;
	OZCStringResource.IDS_OPT_SAVE_MODE = 98;
	OZCStringResource.IDS_OPT_KEEP_FORM_ATTR = 99;
	OZCStringResource.IDS_OPT_IGNORE_COLOR_OUTLINE_CELL = 100;
	OZCStringResource.IDS_OPT_REMOVE_CELL_AFTERDIVIDE = 101;
	OZCStringResource.IDS_OPT_SAVE_SHEET_PER_COLPAGE = 102;
	OZCStringResource.IDS_OPT_SAVE_SHEET_PAGE = 103;
	OZCStringResource.IDS_OPT_COLUMN_FIRST = 104;
	OZCStringResource.IDS_OPT_ROW_FIRST = 105;
	OZCStringResource.IDS_OPT_REMOVE_PAGE_GAP = 106;
	OZCStringResource.IDS_OPT_SIZE_COLON = 107;
	OZCStringResource.IDS_OPT_REMOVE_LINE = 108;
	OZCStringResource.IDS_OPT_EXCLUDE_FIRST_PAGE = 109;
	OZCStringResource.IDS_OPT_ONLY_FIRST_PAGE = 110;
	OZCStringResource.IDS_OPT_ALL_PAGE = 111;
	OZCStringResource.IDS_OPT_LINE_SCOPE_LINE_NUMBER = 112;
	OZCStringResource.IDS_OPT_CHANGE_TXT_TO_INTEGER = 113;
	OZCStringResource.IDS_OPT_APPLY_ONLYLABEL_NUMBER = 114;
	OZCStringResource.IDS_OPT_CHANGE_AS_NORMAL = 115;
	OZCStringResource.IDS_OPT_FONT_COLON = 116;
	OZCStringResource.IDS_OPT_WIDTH = 117;
	OZCStringResource.IDS_OPT_HEIGHT = 118;
	OZCStringResource.IDS_OPT_SAVE_MHT_TYPE = 119;
	OZCStringResource.IDS_OPT_TABLE_SAVE_WAY = 120;
	OZCStringResource.IDS_OPT_TABLE_TYPE_WAY = 121;
	OZCStringResource.IDS_OPT_BAND_SAVE_WAY = 122;
	OZCStringResource.IDS_OPT_BAND_SAVE_PAGE = 123;
	OZCStringResource.IDS_OPT_PPT_SAVE_WAY = 124;
	OZCStringResource.IDS_OPT_MHTD_SAVE_PAGE = 125;
	OZCStringResource.IDS_OPT_FONT_SIZE_SETTING = 126;
	OZCStringResource.IDS_OPT_PAGE_BETWEEN = 127;
	OZCStringResource.IDS_OPT_PER_PAGE_SAVE = 128;
	OZCStringResource.IDS_OPT_LINE_COUNT_COLON = 129;
	OZCStringResource.IDS_OPT_OFFSET_COLON = 130;
	OZCStringResource.IDS_OPT_STARTPOINT_HTMLDOC = 131;
	OZCStringResource.IDS_OPT_ENCODING_COLON = 132;
	OZCStringResource.IDS_OPT_X_AXIS = 133;
	OZCStringResource.IDS_OPT_Y_AXIS = 134;
    OZCStringResource.IDS_OPT_OFFSET = 135;
	OZCStringResource.IDS_OPT_SEPARATION = 136;
	OZCStringResource.IDS_OPT_SEPARATOR = 137;
	OZCStringResource.IDS_OPT_USER_DEFINE = 138;
	OZCStringResource.IDS_OPT_SEPARATION_TYPE = 139;
	OZCStringResource.IDS_OPT_PAGE_BRACKET = 140;
	OZCStringResource.IDS_OPT_SET_PAGELINE = 141;
	OZCStringResource.IDS_OPT_CHAR_NUMBER = 142;
	OZCStringResource.IDS_OPT_ADD_SEPARATOR = 143;
	OZCStringResource.IDS_OPT_REMOVE_SEPARATOR = 144;
	OZCStringResource.IDS_OPT_PAGE_SEPARATION = 145;
	OZCStringResource.IDS_OPT_LABEL_SAVE = 146;
	OZCStringResource.IDS_OPT_LABEL_SIZE = 147;
	OZCStringResource.IDS_OPT_POSITION_SAVE = 148;
	OZCStringResource.IDS_OPT_VERTREL_TOPARA = 149;
	OZCStringResource.IDS_OPT_BACKGROUND_BAND = 150;
	OZCStringResource.IDS_OPT_TITLE_COLON = 151;	
	OZCStringResource.IDS_OPT_DISPLAY_OPTION = 152;
	OZCStringResource.IDS_OPT_GRIDLINES = 153;
	OZCStringResource.IDS_OPT_ENCODING_WAY = 154;
	OZCStringResource.IDS_OPT_ENCODING_G3 = 155;
	OZCStringResource.IDS_OPT_ENCODING_G4 = 156;
	OZCStringResource.IDS_OPT_ENCODING_SAVEAS_ONEFILE = 157;
	OZCStringResource.IDS_OPT_RATIO = 158;
	OZCStringResource.IDS_OPT_ZOOM_INOUT = 159;
	OZCStringResource.IDS_OPT_SET_DPI = 160;
	OZCStringResource.IDS_OPT_DPI = 161;
	OZCStringResource.IDS_OPT_SIZE = 162;
	OZCStringResource.IDS_OPT_SAVE_RATIO = 163;
	OZCStringResource.IDS_OPT_COMPRESSION = 164;
	OZCStringResource.IDS_OPT_QUALITY = 165;
	OZCStringResource.IDS_OPT_USE_INDENT = 166;
	OZCStringResource.IDS_OPT_DATA_SAVE = 167;	
	OZCStringResource.IDS_OPT_FIND_WHAT = 168;
	OZCStringResource.IDS_OPT_MATCH_WHOLE_WORD = 169;
	OZCStringResource.IDS_OPT_MATCH_CASE = 170;
	OZCStringResource.IDS_OPT_DIRECTION = 171;
	OZCStringResource.IDS_OPT_SEARCH_UP = 172;
	OZCStringResource.IDS_OPT_SEARCH_DOWN = 173;
	OZCStringResource.IDS_OPT_NEXT = 174;
	OZCStringResource.IDS_OPT_REFRESH_REPORT = 175;
	OZCStringResource.IDS_OPT_INTERVAL = 176;	
	OZCStringResource.IDS_OPT_HH = 177;
	OZCStringResource.IDS_OPT_MM = 178;
	OZCStringResource.IDS_OPT_SS = 179;
	OZCStringResource.IDS_OPT_REFRESH_NOW = 180;
	OZCStringResource.IDS_OPT_START = 181;
	OZCStringResource.IDS_OPT_STOP = 182;
	OZCStringResource.IDS_OPT_LEFT_COLON = 183;
	OZCStringResource.IDS_OPT_RIGHT_COLON = 184;
	OZCStringResource.IDS_OPT_TOP_COLON = 185;
	OZCStringResource.IDS_OPT_BOTTOM_COLON = 186;
	OZCStringResource.IDS_OPT_PAGE_SETUP = 187;
	OZCStringResource.IDS_OPT_MARGIN = 188;
	OZCStringResource.IDS_OPT_UNIT = 189;
	OZCStringResource.IDS_OPT_UNIT_COLON = 190;
	OZCStringResource.IDS_OPT_MARGIN_OPTION = 191;
	OZCStringResource.IDS_OPT_MARGIN_DEFAULT = 192;
	OZCStringResource.IDS_OPT_MARGIN_AUTO_ADJUST = 193;
	OZCStringResource.IDS_OPT_HTML_VERSION = 194;
	OZCStringResource.IDS_OPT_HTML_VERSION_COLON = 195;
	OZCStringResource.IDS_OPT_TOTAL = 196;
	OZCStringResource.IDS_OPT_REPORT_STRUCTURE = 197;	
	OZCStringResource.IDS_OPT_LABEL_FORMAT = 198;		
	OZCStringResource.IDS_OPT_PARAGRAPH = 199;
	OZCStringResource.IDS_OPT_FILL = 200;
	OZCStringResource.IDS_OPT_LINECOLOR = 201;
	OZCStringResource.IDS_OPT_ETC = 202;	
	OZCStringResource.IDS_OPT_ALIGNMENT = 203;
	OZCStringResource.IDS_OPT_TXT_DIRECTION = 204;
	OZCStringResource.IDS_OPT_INTERNAL_MARGIN = 205;
	OZCStringResource.IDS_OPT_PREVIEW = 206;
	OZCStringResource.IDS_OPT_HORIZON_COLON = 207;
	OZCStringResource.IDS_OPT_VERTICAL_COLON = 208;
	OZCStringResource.IDS_OPT_LINE_SPACE_COLON = 209;
	OZCStringResource.IDS_OPT_INDENT_COLON = 210;
	OZCStringResource.ID_OPT_PARAMETERS_TITLE_LABEL = 211;
	OZCStringResource.IDS_OPT_EXIT = 212;
	OZCStringResource.IDS_OPT_OZZPASSWORD = 213;
	OZCStringResource.IDS_OPT_OZZPASSWORD_CHECK = 214;	
	OZCStringResource.IDS_OPT_ID = 215;
	OZCStringResource.IDS_OPT_SECURITY = 216;	
	OZCStringResource.IDS_OPT_PRODUCT_INFO = 217;
	OZCStringResource.IDS_OPT_OZ_INFO = 218;	
	OZCStringResource.IDS_OPT_ERROR_CODE_COLON = 219;
	OZCStringResource.IDS_OPT_GENERAL_MSG_COLON = 220;
	OZCStringResource.IDS_OPT_VIEW_DETAIL = 221;
	OZCStringResource.IDS_OPT_DETAIL_MSG_COLON = 222;
	OZCStringResource.IDS_OPT_TEXT_CONTROL = 223;
	OZCStringResource.IDS_OPT_SHRINK_TO_FIT = 224;
	OZCStringResource.IDS_OPT_PASSWORD2 = 225;	
	OZCStringResource.IDS_OPT_DM_BARCODE = 226;	
	OZCStringResource.IDS_OPT_FONT_STYLE_COLON = 227;	
	OZCStringResource.IDS_OPT_COLOR_COLON = 228;
	OZCStringResource.IDS_OPT_INTERVAL_COLON = 229;
	OZCStringResource.IDS_OPT_STRETCH_COLON = 230;
	OZCStringResource.IDS_OPT_EFFECT = 231;
	OZCStringResource.IDS_OPT_UNDERLINE = 232;
	OZCStringResource.IDS_OPT_STRIKEOUT = 233;
	OZCStringResource.IDS_OPT_DOUBLE_STRIKEOUT = 234;
	OZCStringResource.IDS_OPT_NO_FILL = 235;
	OZCStringResource.IDS_OPT_TRANSPARENCY = 236;
	OZCStringResource.IDS_OPT_GRADIENT = 237;
	OZCStringResource.IDS_OPT_USE_GRADIENT = 238;
	OZCStringResource.IDS_OPT_SLASH = 239;
	OZCStringResource.IDS_OPT_BACK_SLASH = 240;
	OZCStringResource.IDS_OPT_START_POSITION = 241;
	OZCStringResource.IDS_OPT_FROM_EDGE = 242;
	OZCStringResource.IDS_OPT_DIRECT = 243;
	OZCStringResource.IDS_OPT_FROM_CENTER = 244;
	OZCStringResource.IDS_OPT_REVERSE = 245;
	OZCStringResource.IDS_OPT_THICKNESS_W = 246;
	OZCStringResource.IDS_OPT_STYLE_Y = 247;
	OZCStringResource.IDS_OPT_COLOR_C = 248;
	OZCStringResource.IDS_OPT_CLIPPING = 249;
	OZCStringResource.IDS_OPT_WORD_WRAP = 250;
	OZCStringResource.IDS_OPT_WORD_WRAP_TYPE = 251;
	OZCStringResource.IDS_OPT_IGNORE_SPACE = 252;
	OZCStringResource.IDS_OPT_NON_ASCII_WORD = 253;
	OZCStringResource.IDS_OPT_TOOLTIP_TEXT = 254;
	OZCStringResource.IDS_OPT_BASIC = 255;
	OZCStringResource.IDS_OPT_OUTLINE = 256;
	OZCStringResource.IDS_OPT_3D = 257;
	OZCStringResource.IDS_OPT_ENGRAVE = 258;
	OZCStringResource.IDS_OPT_SEGMENT = 259;
	OZCStringResource.IDS_OPT_HOLLOW = 260;
	OZCStringResource.IDS_OPT_SHADOW = 261;
	OZCStringResource.IDS_OPT_IMAGE_STYLE = 262;	
	OZCStringResource.IDS_OPT_BARCODE_STYLE = 263;
	OZCStringResource.IDS_OPT_DATA_GAP = 264;
	OZCStringResource.IDS_OPT_DATA_POSITION = 265;
	OZCStringResource.IDS_OPT_BARCODE_MARGIN = 266;
	OZCStringResource.IDS_OPT_ALIGN_HORIZONTALLY = 267;
	OZCStringResource.IDS_OPT_RATION_COLON = 268;
	OZCStringResource.IDS_OPT_PRINTING_SCALE = 269;
	OZCStringResource.IDS_OPT_ROTATION_COLON = 270;
	OZCStringResource.IDS_OPT_TITLE_GAP = 271;
	OZCStringResource.IDS_OPT_TITLE_POSITION = 272;
	OZCStringResource.IDS_OPT_BARCODE_ERROR_OPTION = 273;
	OZCStringResource.IDS_OPT_DISPLAY_ERROR_DATA = 274;
	OZCStringResource.IDS_OPT_DISPLAY_ERROR_SIZE = 275;
	OZCStringResource.IDS_OPT_DISPLAY_EMPTY_DATA = 276;
	OZCStringResource.IDS_OPT_PDF_IMAGE_STYLE = 277;
	OZCStringResource.IDS_OPT_PDF_ROW_NUMBER = 278;
	OZCStringResource.IDS_OPT_PDF_COL_NUMBER = 279;
	OZCStringResource.IDS_OPT_PDF_ECC = 280;
	OZCStringResource.IDS_OPT_PDF_X_SCALE = 281;
	OZCStringResource.IDS_OPT_PDF_Y_SCALE = 282;
	OZCStringResource.IDS_OPT_BORDER_COLOR = 283;
	OZCStringResource.IDS_OPT_FILL_COLOR = 284;
	OZCStringResource.IDS_OPT_THICKNESS = 285;
	OZCStringResource.IDS_OPT_SHAPE_NO_FILL = 286;
	OZCStringResource.IDS_OPT_LINE_TYPE = 287;
	OZCStringResource.IDS_OPT_TOOLTIP_TEXT_COLON = 288;
	OZCStringResource.IDS_OPT_EDIT_CHART = 290;
	OZCStringResource.IDS_OPT_SHOW_DATA = 291;
	OZCStringResource.IDS_OPT_EDIT_SHAPE = 292;
	OZCStringResource.IDS_FORCS_RV = 293;
	OZCStringResource.IDS_OPT_EXCEL_LINE = 294;
	OZCStringResource.IDS_OPT_EXCEL_SAVE_TYPE = 295;
	OZCStringResource.IDS_OPT_JPG_ZOOM_INOUT = 296;
	OZCStringResource.IDS_OPT_ZOOM_INOUT_COLON = 297;
	OZCStringResource.IDS_OPT_QUALITY_COLON = 298;	
	OZCStringResource.IDS_OPT_TIFF_SIZE = 299;
	OZCStringResource.IDS_OPT_PAGELINE = 300;
	OZCStringResource.IDS_OPT_WORD_BAND_SAVE_PAGE = 301;
	OZCStringResource.IDS_OPT_PRINT_MULTIDOC = 302;
	OZCStringResource.ID_PAGE_CONTROL_WINDOW = 303;
	OZCStringResource.IDS_OPT_FONT_NAME_COLON = 304;
	OZCStringResource.IDS_OPT_FONT_OPTION = 305;
	OZCStringResource.IDS_OPT_AUTO_SELECTED = 306;
	OZCStringResource.IDS_OPT_SIDE = 307;
	OZCStringResource.IDS_OPT_LENGTH = 308;
	OZCStringResource.IDS_OPT_OTHER = 309;
	OZCStringResource.IDS_OPT_FONT_SAVE_WAY = 310;
	OZCStringResource.IDS_OPT_HORIZONTAL_NAVI = 311;
	OZCStringResource.IDS_OPT_VERTICAL_NAVI = 312;
	OZCStringResource.IDS_OPT_PNG_SAVE = 313;
	OZCStringResource.IDS_OPT_GIF_SAVE = 314;
	OZCStringResource.IDS_OPT_REMOVE_LINES = 315;	
	OZCStringResource.IDS_OPT_ENCODING_HDM = 316;
	OZCStringResource.IDS_OPT_SVG_SAVE = 317;
	OZCStringResource.IDS_OPT_PDF_CHANGEABLE = 318;
	OZCStringResource.IDS_OPT_PDF_RADIO_CHANGEABLE1 = 319;
	OZCStringResource.IDS_OPT_PDF_RADIO_CHANGEABLE2 = 320;	
	OZCStringResource.IDS_OPT_PDF_RADIO_CHANGEABLE3 = 321;
	OZCStringResource.IDS_OPT_PDF_RADIO_CHANGEABLE4 = 322;
	OZCStringResource.IDS_OPT_SAVE_WAY_BASE = 323;
	OZCStringResource.IDS_OPT_SAVE_LARGEBUNDLE_BYPAGE = 324;
	OZCStringResource.IDS_OPT_SAVE_LARGE_BUNDLE = 325;	
	OZCStringResource.IDS_OPT_SAVE_BY_PAGE = 326;
	OZCStringResource.IDS_OPT_KEEP_SAVING = 327;
	OZCStringResource.IDC_TITLE_SECURITY = 328;	
	OZCStringResource.IDC_EDIT_PASSWORD = 329;
	OZCStringResource.IDC_EDIT_PASSWORD_VERIFY = 330;
	OZCStringResource.IDC_ZIP_GROUP = 331;
	OZCStringResource.IDC_COMPRESS_ON_SAVE = 332;
	OZCStringResource.IDC_PAPER_SETUP = 333;
	OZCStringResource.IDC_CHECK_ADJUST = 334;	
	OZCStringResource.IDS_OPT_CHOOSEPAGE_DETAIL = 335;	
	OZCStringResource.IDS_OPT_CHOOSEPAGE_DETAIL2 = 336;
	OZCStringResource.IDS_OPT_CHOOSEPAGE_DETAIL2 = 336;
	OZCStringResource.IDS_OPT_NEXCEL_SAVE = 337;


	OZCStringResource.IDS_OPT_EXCEL_SAVE2 = 338;
	OZCStringResource.IDS_CHECK_EXCEL_ZOOM = 339;
	OZCStringResource.IDS_CHECK_EXCEL_ZOOM_RATIO = 340;
	OZCStringResource.IDS_CHECK_EXCEL_ZOOM_RATIO2 = 341;
	OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES = 342;
	OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES_ROW = 343;
	OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES_COL = 344;
	OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES_ROW_M = 345;
	OZCStringResource.IDS_CHECK_EXCEL_FREEZEPANES_COL_M = 346; 
		
};
OZCStringResource.__define_class__ = function() {
	__define__(OZCStringResource);
	var _this = __prototype__(OZCStringResource);
	_this.__constructor__OZCStringResource = function() {
	};
};
__class__["OZCStringResource"] = OZCStringResource;
}