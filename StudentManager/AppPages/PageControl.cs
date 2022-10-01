namespace StudentManager.AppPages
{
	public class PageControl
	{
		private static GroupPage groupPage;
		private static StudentPage studentPage;
		private static AuthorizationPage authorizationPage;
		private static SpecialtyPage specialtyPage;

		public static AuthorizationPage AuthorizationPage
		{
			get
			{
				if (authorizationPage == null)
				{
					authorizationPage = new AuthorizationPage();
				}
				return authorizationPage;
			}
		}
		public static SpecialtyPage SpecialtyPage
		{
			get
			{
				if (specialtyPage == null)
				{
					specialtyPage = new SpecialtyPage();
				}
				return specialtyPage;
			}
		}
		public static GroupPage GroupPage
		{
			get
			{
				if (groupPage == null)
				{
					groupPage = new GroupPage();
				}
				return groupPage;
			}
		}
		public static StudentPage StudentPage
		{
			get
			{
				if (studentPage == null)
				{
					studentPage = new StudentPage();
				}
				return studentPage;
			}
		}
	}
}
