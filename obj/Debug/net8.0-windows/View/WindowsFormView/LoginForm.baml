   M S B A M L   `   `   `  ����2  .STUDENT-VERIFICATION-SYSTEM-THIRD-YEAR-PROJECTS    MSTUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.WindowsFormView.LoginForm   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -   5          � Title$  Login��5   	   $	��457��5   	   $	��705��5	   	    � WindowStartupLocation$ CenterScreen=�5
   	    � WindowStyle$
 None=�5   	   
������  � AllowsTransparency$
 True��5   	    � 
ResizeMode$ NoResize=�5   	   .��5      � .+�5   
   �� ���#3D7E35 �� CornerRadius$ 10��5      $��2q�5      
�������._�5      �  � RowDefinitions 5!      �� 5#      .+�5*      �� $ 10��6   ��5+      �� 5,       j� Stretch$
 Fill=�6%    �� ImageSource$4 .pack://application:,,,/Resources/UmPicture.png��64   5/      �� 56      -   5        	 ��Name$	 CloseBtn��56         MinimizeButtonStyle#��[  6+   $��X��57      $��671,0,0,423q�59      $��30��5:      $��30��5;      5@      �� 5E      $	��300��6   $	��400��5F      
�������
 J� Opacity$

 0.93Q�5H      $ 10��5I      ._�5M      �� .+�5O      �� $1��+pack://application:,,,/Resources/LogoUm.png��6    $	��100��5P       $��0,8,0,0q�5Q       �� 5V      $��Welcome��6$   $��12(�5W   $   $��DemiBold"�5X   $   $��52��5Y   $   $��0,10,0,0q�5Z   $   �� 5^      -   5         ��Name$ Usernametxt��5^   "     	TextBoxes#��[ 67   $	��180��5_   "   $
��0,40q�5`   "   
�������5a   "   G� 5f      -   5         G�Name$ Passwordtxt��5f   &     PasswordBoxes#��[ 6;   $	��180��5g   "   $��	0,-10,0,0q�5h   "   
�������5i   "   �� 5o      $��forgot password?��6$   $��Italic$�5p   $   $��	Underline{�5q   $   $	��100��5r   $   $��12(�5s   $   $��0,10,0,0q�5t   $    � Cursor$
 Hand��5u   $   �� 5x      -   5        $	 LoginBtn��5x   !     SubmitButton#��[ 63   $	��190��5y   !   $��0,20,0,0q�5z   !   $��Center=�5{   !   $��0q�5|   !   $��Login��5}   !   $��DemiBold"�5~   !   5   !   5�      5�      5�      5�      5�      5�      