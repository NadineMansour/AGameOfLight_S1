class StudentsController < ApplicationController
	def show
    @user = Student.find(params[:id])
  end



  def new
    @user = Student.new
  end


  def view_course_teachers

    @teachers=Teacher.where(school: Student.find(1).school)

  end
  def send_message
    @message=Message.new
    session[:y]=params[:teacher_id]
  end 

  def submit 
      @message=Message.new
      @message.semail=Student.find(1)
      @message.remail=session[:y]
      @message.text=params[:my_input]
      session = {}
      @message.save
      redirect_to view_courses_students_path
  end 

  def view_courses
   @subjects = Subject.where(school: Student.find(1).school)
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



  private
    def user_params
      params.require(:student).permit(:email, :password,
                                   :password_confirmation)
    end
end
