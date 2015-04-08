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
