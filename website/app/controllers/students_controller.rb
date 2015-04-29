class StudentsController < ApplicationController
	def show
    @user = Student.find(params[:id])
  end


  def new
    @user = Student.new
  end


  def view_courses
   @subjects = Subject.where(school: current_student.school)
  end


  def view_course_teachers

    @teachers=Teacher.where(school: current_student.school)

  end


  def send_message
    @message=Mess.new
    @var=params[:teacher_id]
  end 


  def submit 
      @message=Mess.new
      @message.semail=current_student.email
      @message.remail=Teacher.find(params[:teacher_id]).email
      @message.text=params[:my_input]

      @message.save
      redirect_to view_courses_students_path
  end 

  
  def show_course
   @subject = Subject.find(params[:subject_id])
  end


  def create
    @user = Student.new(user_params)
    if @user.save
      log_in @user
      flash[:success] = "Welcome!"
      redirect_to("home#index")
    else
      render 'new'
    end
  end

  def view_contacts
  @user=current_student

  @students= Student.where(school:current_student.school)


  end

  def view_messages
    @user=current_student
    @sender=Student.find(params[:student_id]) 
    @message=Mess.where(semail:@sender.email)

  end

  def view_messages_teacher
    @user=current_student
    @sender=Teacher.find(params[:teacher_id]) 
    @message=Mess.where(semail:@sender.email)

  end


  private
    def user_params
      params.require(:student).permit(:email, :password,
                                   :password_confirmation)
    end
end
