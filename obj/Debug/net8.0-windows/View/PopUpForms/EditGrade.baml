   M S B A M L   `   `   `  ����i  eSTUDENT-VERIFICATION-SYSTEM-THIRD-YEAR-PROJECT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�Lclr-namespace:STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.PopUpForms>STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.PopUpForms  5   	   N    HSTUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.PopUpForms.EditGrade   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ^localLclr-namespace:STUDENT_VERIFICATION_SYSTEM_THIRD_YEAR_PROJECT.View.PopUpForms     5   	     � Title$  	EditGrade��5   	    � WindowStartupLocation$ CenterScreen=�5	   	    � WindowStyle$
 None=�5
   	    � AllowsTransparency$
 True��5   	   
������ $	��200��5   	   $	��400��5   	   .��5      ��  �� CornerRadius$ 10��6   $��1q�6   ���#DDDDDD���#FFFFFF._�5   
   �  � RowDefinitions 5      �� 5      $��50 �6    �� 5      $��* �6    �� 5      $��30 �6    .+�5      �� $��0��6   ���#3D7E35$ 	10,10,0,0��67   ._�5      � .+�5      �� -   5         ��Name$ GradeTxt��5       $��
Edit Grade��62   $��18(�6D   $��SemiBold"�6R   
�������$��Center=�6{   $��20,0,0,0q�6�   �� 5      -   5         ��Name$ CloseButton��5      $	��✕��5      $��14(�6)   $
��Bold"�5      
�������
������ $��0q�5!      $��Right=�5"      $��Center=�5#      $��0,0,15,0q�68    � Cursor$
 Hand��6J   5$      5%      5&      � 5)      $��1��6   $��20q�6    .+�5*      �� $��Center=�6   .+�5+      �� $��Enter New Grade:��6    $��16(�5,       $��0,0,0,10q�5-       �� 5/      -   5        	 ��Name$	 GradeTextBox��5/      $��40��50      $��16(�51      $
��10,5q�52      
 �� 	MaxLength$
 10��53      ���#F8F8F8���#CCCCCC$��1q�57      ��54      �� *Selected_grades.GradeValue+.��59       � 	Resources 5:       �� 
TargetType �� Property �� Value��%^   '��       �� 5;      " ��6$   .�5<   "   ��  �@��CornerRadius$ 5��6A   5=      5>      .��6-   5?      5@      5A      5C      5D      5E      